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
    public class ProductBL : GenericBusinessLogic<Product>, IProductBL
    {
        public ProductBL(MyContext context) : base(context) 
        {
            //this.repository = DBFactoryInstance.GetFactoryInstance.CreateProductRepository(context);
        }

        public ProductBL(IRepository<Product> repository) : base(repository)
        {            
        }        
    }
}
