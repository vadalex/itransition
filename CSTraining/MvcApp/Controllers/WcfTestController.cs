using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ServiceClient.ProductService;


namespace MvcApp.Controllers
{
    public class WcfTestController : Controller
    {
        public ActionResult Index()
        {
            using (ProductServiceClient service = new ProductServiceClient())
            {
                IEnumerable<Product> products = service.GetAllProducts();
                return View(products);
            }
        }
    }
}