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
    public class SupplierBL : GenericBusinessLogic<Supplier>, ISupplierBL
    {      

        public SupplierBL(MyContext context) : base(context) 
        {
            //this.repository = DBFactoryInstance.GetFactoryInstance.CreateSupplierRepository(context);
        }

        public SupplierBL(IRepository<Supplier> repository) : base(repository)
        {            
        }        
    }
}
