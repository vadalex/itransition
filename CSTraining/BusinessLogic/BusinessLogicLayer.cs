using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.BLL;
using DAL;
using Contracts.BLL;
using Microsoft.Practices.Unity;

namespace BusinessLogic
{
    public class BusinessLogicLayer : IBusinessLogicLayer
    {
        [Dependency]
        public ISupplierBL Suppliers { get; set; }

        [Dependency]
        public IProductBL Products { get; set; }

        [Dependency]
        public IOrderBL Orders { get; set; }

        [Dependency]
        public ICustomerBL Customers { get; set; }

        [Dependency]
        public IUserBL Users { get; set; }

        [Dependency]
        public ICommentBL Comments { get; set; }

        public BusinessLogicLayer()
        {               
            /*
            AbstractBLFactory factory = BLFactoryInstance.GetFactoryBLInstance();
            this.Suppliers = factory.CreateSupplierBL();
            this.Products = factory.CreateProductBL();
            this.Orders = factory.CreateOrderBL();
            this.Customers = factory.CreateCustomerBL();
             */
        }        
    }
}
