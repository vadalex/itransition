using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lucene.Net.Documents;
using Lucene.Net.Analysis;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net;
using Entities;
using Lucene.Net.Store;
using Lucene.Net.Analysis.Standard;
using System.Configuration;
using Entities.Attributes;
using Version = Lucene.Net.Util.Version;
using System.Reflection;

namespace BusinessLogic.Search
{
    public class LuceneSearch
    {
        private FSDirectory directory;
        private int maxHits; 
        private static List<string> fields = new List<string>();

        public LuceneSearch()
        {
            string indexPath = ConfigurationManager.AppSettings["LuceneIndexStoragePath"];
            this.directory = FSDirectory.Open(indexPath);
            maxHits = Int32.MaxValue;
        }

        public void AddIndex<T>(T entity)
        {
            AddIndexes<T>(new List<T>() { entity });
        }       

        public void AddIndexes<T>(IEnumerable<T> entities)
        {
            AddLucenePropertiesToFieldList<T>();
            var analyzer = new StandardAnalyzer(Version.LUCENE_30);
            using (var writer = new IndexWriter(directory, analyzer, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                foreach (var entity in entities)
                    AddIndex(entity, writer);
                analyzer.Close();
            }
        }

        public void AddIndex<T>(T entity, IndexWriter writer)
        {
            PropertyInfo idProperty = GetIdProperty<T>();
            var searchQuery = new TermQuery(new Term(idProperty.Name, idProperty.GetValue(entity).ToString()));
            writer.DeleteDocuments(searchQuery);
            Document doc = new Document();
            List<PropertyInfo> properties = GetLuceneProperties<T>();
            foreach (var prop in properties)
            {
                doc.Add(new Field(prop.Name, prop.GetValue(entity).ToString(), Field.Store.YES, prop.GetCustomAttribute<LuceneSearchAttribute>().Index));                
            }
            doc.Add(new Field("Type", typeof(T).Name, Field.Store.YES, Field.Index.NO));
            writer.AddDocument(doc);
        }

        private PropertyInfo GetIdProperty<T>()
        {
            return GetLuceneProperties<T>().FirstOrDefault(p => p.GetCustomAttribute<LuceneSearchAttribute>().IsIdentity);
        }

        private List<PropertyInfo> GetLuceneProperties<T>()
        {
            return typeof(T).GetProperties().Where(p => p.GetCustomAttributes(typeof(LuceneSearchAttribute), false).Any()).ToList(); 
        }

        private void AddLucenePropertiesToFieldList<T>()
        {
            var properties =  typeof(T).GetProperties().Where(p => p.GetCustomAttributes(typeof(LuceneSearchAttribute), false).Any()).Select(p => p.Name).ToList();
            foreach (var name in properties)
            {
                if (!fields.Any(f => f == name))
                    fields.Add(name);
            }
        }

        public void ClearLuceneIndex()
        {            
            var analyzer = new StandardAnalyzer(Version.LUCENE_30);
            using (var writer = new IndexWriter(directory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                writer.DeleteAll();
                analyzer.Close();
            }
        }

        #region MapEntities
        private Product MapProduct(Document doc)
        {
            Product product = new Product
            {
                ProductID = Convert.ToInt32(doc.Get("ProductID")),
                Name = doc.Get("Name"),
                Category = doc.Get("Category"),
                Price = Convert.ToDouble(doc.Get("Price"))
            };
            return product;
        }

        private Supplier MapSupplier(Document doc)
        {
            Supplier supplier = new Supplier
            {
                SupplierID = Convert.ToInt32(doc.Get("SupplierID")),
                Name = doc.Get("Name"),
                Details = doc.Get("Details")
            };

            return supplier;
        }

        private Order MapOrder(Document doc)
        {
            Order order = new Order
            {
                OrderID = Convert.ToInt32(doc.Get("OrderID")),
                Details = doc.Get("Details"),
                CustomerID = Convert.ToInt32(doc.Get("CustomerID"))
            };
            return order;
        }

        private Customer MapCustomer(Document doc)
        {
            Customer customer = new Customer
            {
                CustomerID = Convert.ToInt32(doc.Get("CustomerID")),
                Name = doc.Get("Name"),
                Address = doc.Get("Address")
            };
            return customer;
        }
        #endregion


        private SearchResult MapEntities(IEnumerable<ScoreDoc> hits, IndexSearcher searcher)
        {
            var searchResult = new SearchResult(); 
            var docs = hits.Select(hit => searcher.Doc(hit.Doc)).ToList();
            var productDocs = docs.Where(d => d.Get("Type") == typeof(Product).Name.ToString()).ToList();
            var supplierDocs = docs.Where(d => d.Get("Type") == typeof(Supplier).Name.ToString()).ToList();
            var orderDocs = docs.Where(d => d.Get("Type") == typeof(Order).Name.ToString()).ToList();
            var customerDocs = docs.Where(d => d.Get("Type") == typeof(Customer).Name.ToString()).ToList();
            if(productDocs.Any())
                searchResult.Products = productDocs.Select(MapProduct).ToList();
            if(supplierDocs.Any())
                searchResult.Suppliers = supplierDocs.Select(MapSupplier).ToList();
            if (orderDocs.Any())
                searchResult.Orders= orderDocs.Select(MapOrder).ToList();
            if (customerDocs.Any())
                searchResult.Customers = customerDocs.Select(MapCustomer).ToList();
            return searchResult;
        }

        public SearchResult Search(string searchQuery)
        {   
            if (string.IsNullOrEmpty(searchQuery))
                return new SearchResult() { KeyWords = searchQuery };
            var terms = searchQuery.Trim().Replace("-", " ").Split(' ').Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Trim());
            searchQuery = string.Join(" ", terms);
            using (var searcher = new IndexSearcher(directory, false))
            {
                var analyzer = new StandardAnalyzer(Version.LUCENE_30);
                var parser = new MultiFieldQueryParser(Version.LUCENE_30, fields.ToArray(), analyzer);
                var query = parser.Parse(searchQuery);
                ScoreDoc[] hits = searcher.Search(query, null, maxHits, Sort.RELEVANCE).ScoreDocs;
                SearchResult results = MapEntities(hits, searcher);
                results.KeyWords = searchQuery;
                analyzer.Close();
                return results;
            }
        }
    }   
}