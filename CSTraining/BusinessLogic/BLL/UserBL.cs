using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.BLL;
using Contracts.DAL;
using DAL;
using Entities;

namespace BusinessLogic.BLL
{
    public class UserBL : GenericBusinessLogic<User>, IUserBL
    {

        public UserBL(MyContext context)
            : base(context)
        {
            //this.repository = DBFactoryInstance.GetFactoryInstance.CreateSupplierRepository(context);
        }

        public UserBL(IRepository<User> repository)
            : base(repository)
        {
        }
    }
}
