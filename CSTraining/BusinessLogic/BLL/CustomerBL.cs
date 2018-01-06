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
    public class CustomerBL : GenericBusinessLogic<Customer>, ICustomerBL
    {
        public CustomerBL(MyContext context) : base(context) 
        {
            //this.repository = DBFactoryInstance.GetFactoryInstance.CreateCustomerRepository(context);
        }
    
        public CustomerBL(IRepository<Customer> repository) : base(repository)
        {            
        }        
    }
}
