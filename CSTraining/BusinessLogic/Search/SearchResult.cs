using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Search
{
    public class SearchResult
    {
        public SearchResult()
        {
            Products = new HashSet<Product>();
            Suppliers = new HashSet<Supplier>();
            Orders = new HashSet<Order>();
            Customers = new HashSet<Customer>();
        }

        public string KeyWords { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Supplier> Suppliers { get; set; }
        public IEnumerable<Order> Orders { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
    }
}
