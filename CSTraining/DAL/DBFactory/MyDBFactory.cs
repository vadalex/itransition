using Contracts.DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MyDBFactory : AbstractDBFactory
    {
        public override IRepository<Customer> CreateCustomerRepository(MyContext context)
        {
            return new DataRepository<Customer>(context);
        }

        public override IRepository<Order> CreateOrderRepository(MyContext context)
        {
            return new DataRepository<Order>(context);
        }
        public override IRepository<Product> CreateProductRepository(MyContext context)
        {
            return new DataRepository<Product>(context);
        }

        public override IRepository<Supplier> CreateSupplierRepository(MyContext context)
        {
            return new DataRepository<Supplier>(context);
        }
    }
}
