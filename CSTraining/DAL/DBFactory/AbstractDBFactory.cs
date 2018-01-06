using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.DAL;
using Entities;

namespace DAL
{
    public abstract class AbstractDBFactory
    {        
        public abstract IRepository<Customer> CreateCustomerRepository(MyContext context);
        public abstract IRepository<Order> CreateOrderRepository(MyContext context);
        public abstract IRepository<Product> CreateProductRepository(MyContext context);
        public abstract IRepository<Supplier> CreateSupplierRepository(MyContext context);
    }
}
