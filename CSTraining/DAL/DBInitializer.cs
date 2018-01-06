using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Entities;
namespace DAL
{
    public class DBInitializer : DropCreateDatabaseIfModelChanges<MyContext>
    {
        protected override void Seed(MyContext context)
        {
            base.Seed(context);

            var suppliers = new List<Supplier> {
                new Supplier{ Name = "supplier1", Phone = "phone1", Details = "detatils1" },
                new Supplier{ Name = "supplier2", Phone = "phone2", Details = "detatils2" },
                new Supplier{ Name = "supplier3", Phone = "phone3", Details = "detatils3" },
                new Supplier{ Name = "supplier4", Phone = "phone4", Details = "detatils4" }
            };
            suppliers.ForEach(s => context.Suppliers.Add(s));
            context.SaveChanges();

            var products = new List<Product> {
                new Product { Name = "product1", SupplierID = 1, Category = "category1", Price = 10 },
                new Product { Name = "product2", SupplierID = 1, Category = "category2", Price = 20 },
                new Product { Name = "product3", SupplierID = 2, Category = "category1", Price = 30 },
                new Product { Name = "product4", SupplierID = 2, Category = "category3", Price = 40 },
                new Product { Name = "product5", SupplierID = 3, Category = "category2", Price = 50 },
                new Product { Name = "product6", SupplierID = 3, Category = "category3", Price = 60 },
                new Product { Name = "product7", SupplierID = 4, Category = "category2", Price = 70 },
                new Product { Name = "product8", SupplierID = 4, Category = "category1", Price = 80 }
            };
            products.ForEach(p => context.Products.Add(p));
            context.SaveChanges();

            var customers = new List<Customer> {
                new Customer { Name = "customer1", Address = "address1", Phone = "phone1"},
                new Customer { Name = "customer2", Address = "address2", Phone = "phone2"},
                new Customer { Name = "customer3", Address = "address3", Phone = "phone3"},
                new Customer { Name = "customer4", Address = "address4", Phone = "phone4"}
            };
            customers.ForEach(c => context.Customers.Add(c));
            context.SaveChanges();

            var orders = new List<Order> {
                new Order { Status = "status1", CustomerID = 1, Details = "details1", Products = new HashSet<Product> {context.Products.Find(1), context.Products.Find(3)} }, 
                new Order { Status = "status1", CustomerID = 2, Details = "details2", Products = new HashSet<Product> {context.Products.Find(2), context.Products.Find(8)} }, 
                new Order { Status = "status1", CustomerID = 3, Details = "details3", Products = new HashSet<Product> {context.Products.Find(3)} }, 
                new Order { Status = "status1", CustomerID = 4, Details = "details4", Products = new HashSet<Product> {context.Products.Find(1), context.Products.Find(4), context.Products.Find(7)} }, 
                new Order { Status = "status1", CustomerID = 2, Details = "details5", Products = new HashSet<Product> {context.Products.Find(5), context.Products.Find(6)} },
                new Order { Status = "status1", CustomerID = 4, Details = "details6", Products = new HashSet<Product> {context.Products.Find(7), context.Products.Find(2), context.Products.Find(8), context.Products.Find(3)} }
            };
            orders.ForEach(o => context.Orders.Add(o));
            context.SaveChanges();
        }

       

    }
}
