using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.BLL;

namespace BusinessLogic
{
    public abstract class AbstractBLFactory
    {
        public abstract ICustomerBL CreateCustomerBL();
        public abstract IOrderBL CreateOrderBL();
        public abstract IProductBL CreateProductBL();
        public abstract ISupplierBL CreateSupplierBL();
    }
}
