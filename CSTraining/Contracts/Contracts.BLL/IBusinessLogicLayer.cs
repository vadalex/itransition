using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts.BLL
{
    public interface IBusinessLogicLayer
    {
        ISupplierBL Suppliers { get; set; }

        IProductBL Products { get; set; }
        
        IOrderBL Orders { get; set; }
        
        ICustomerBL Customers { get; set; }
        
        IUserBL Users { get; set; }

        ICommentBL Comments { get; set; }
    }
}
