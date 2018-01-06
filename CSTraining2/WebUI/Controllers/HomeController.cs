using System.Web.Mvc;

namespace WebUI.Controllers
{
    public class HomeController : Controller
    {        
        public ActionResult Index()
        {           
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }
    }

    
}