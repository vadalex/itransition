using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using Entities;

namespace WcfService
{
    [ServiceContract]
    public interface IProductService
    {

        [OperationContract]
        IEnumerable<Product> GetAllProducts();

        [OperationContract]
        Product GetProduct(int id);

        [OperationContract]
        bool CreateProduct(Product product);

        [OperationContract]
        bool UpdateProduct(Product product);

        [OperationContract]
        bool DeleteProduct(int id);

        [OperationContract]
        bool CreateSupplier(Supplier supplier);
    }
}
