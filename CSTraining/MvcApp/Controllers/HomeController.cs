using System.Linq;
using System.Web.Mvc;
using BusinessLogic.Search;
using Contracts.BLL;
using DAL;
using Entities;
using MvcApp.Filters;
using WebMatrix.WebData;

namespace MvcApp.Controllers
{
    [InitializeSimpleMembership]
    public class HomeController : BaseController
    {
        public HomeController(IBusinessLogicLayer bll) : base(bll)
        {
        }

        public ActionResult Index()
        {
            //BusinessLogicLayer bll = ((HttpContext.ApplicationInstance as MvcApplication).Application["Unity"] as IUnityContainer).Resolve<BusinessLogicLayer>(); 
            //BusinessLogicLayer bll = DependencyResolver.Current.GetService<BusinessLogicLayer>();            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Error()
        {
            HandleErrorInfo error = Session["LastError"] as HandleErrorInfo;            
            return View(error); 
        }

        public ActionResult SearchResult(string keyWords)
        {
            var lucene = new LuceneSearch();
            SearchResult results = lucene.Search(keyWords);            
            return View(results);
        }

        [HttpPost]
        public JsonResult UpdateLucene()
        {
            var lucene = new LuceneSearch();
            lucene.AddIndexes(bll.Products.GetAll());
            lucene.AddIndexes(bll.Customers.GetAll());
            lucene.AddIndexes(bll.Orders.GetAll());
            lucene.AddIndexes(bll.Suppliers.GetAll());
            return Json((object)true);
        }

        public ActionResult MyOrders()
        {
            var customerId = bll.Customers.GetAll(c => c.UserID == WebSecurity.CurrentUserId).FirstOrDefault().CustomerID;
            var orders = bll.Orders.GetAll(o => o.CustomerID == customerId).OrderByDescending(o => o.CreatedDate).ToList();
            return View(orders);
        }
    }
}
