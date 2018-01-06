using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TodoWebUI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AngularTodoList()
        {
            return View();
        }

        public ActionResult BackboneTodoList()
        {
            return View();
        }

        public ActionResult ReactJSTodoList()
        {
            return View();
        }

    }
}
