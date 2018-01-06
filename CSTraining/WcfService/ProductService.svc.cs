using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Contracts.DAL;
using DAL;
using Entities;

namespace WcfService
{
    
    public class ProductService : IProductService
    {
        
        private IRepository<Product> repository;
        private IRepository<Supplier> suppliers;

        public ProductService()
        {
            var context = new MyContext();
            context.Configuration.ProxyCreationEnabled = false;
            repository = new DataRepository<Product>(context);
            suppliers = new DataRepository<Supplier>(context);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = repository.GetAll().ToList();
            return products;
        }

        public Product GetProduct(int id)
        {
            Product product = repository.GetSingle(id);
            return product;
        }
        public bool CreateProduct(Product product)
        {
            try
            {
                repository.Add(product);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool UpdateProduct(Product product)
        {
            try
            {
                repository.Update(product);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool DeleteProduct(int id)
        {
            try
            {
                var product = repository.GetSingle(id);
                repository.Remove(product);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }

        public bool CreateSupplier(Supplier supplier)
        {
            try
            {
                suppliers.Add(supplier);
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
    }
}

