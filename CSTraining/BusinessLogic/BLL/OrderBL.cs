using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities;
using DAL;
using Contracts.BLL;
using Contracts.DAL;

namespace BusinessLogic.BLL
{
    public class OrderBL : GenericBusinessLogic<Order>, IOrderBL
    {
        public OrderBL(MyContext context) : base(context) 
        {            
            //this.repository = DBFactoryInstance.GetFactoryInstance.CreateOrderRepository(context);
        }

        public OrderBL(IRepository<Order> repository) : base(repository)
        {            
        }        
    }
}
