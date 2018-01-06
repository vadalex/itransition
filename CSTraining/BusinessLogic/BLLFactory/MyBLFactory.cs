using Contracts.DAL;
using DAL;
using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.BLL;
using BusinessLogic.BLL;

namespace BusinessLogic
{
    public class MyBLFactory : AbstractBLFactory
    {
        private MyContext context = new MyContext();

        public override ICustomerBL CreateCustomerBL()
        {
            return new CustomerBL(context);
        }

        public override IOrderBL CreateOrderBL()
        {
            return new OrderBL(context);
        }
        public override IProductBL CreateProductBL()
        {
            return new ProductBL(context);
        }

        public override ISupplierBL CreateSupplierBL()
        {
            return new SupplierBL(context);
        }
    }
}
