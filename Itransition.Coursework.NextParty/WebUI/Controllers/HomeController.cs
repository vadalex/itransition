using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Filters;
using WebUI.Models;
using WebUI.Models.Entities;
using WebUI.Models.DataAccessLayer;
using WebUI.Models.Helpers;


namespace WebUI.Controllers
{
    [InitializeSimpleMembership]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var rep = new DataRepositories();
            var events = rep.Events.GetAll();
            var worst = events.ToList().OrderBy(e => e.GetAlcoholForParticipant()).Take(5).ToList();
            var best = events.ToList().OrderByDescending(e => e.GetAlcoholForParticipant()).Take(5).ToList();
            var future = events.ToList().Where(e => !e.IsPassedEvent()).ToList();
            var vm = new IndexViewModel()
            {
                BestEvents = best,
                WorstEvents = worst,
                AllFutureEvents = future
            };
            return View(vm);
        }
                
        public JsonResult GetFutureEvents()
        {
            var rep = new DataRepositories();
            var events = rep.Events.GetAll().ToList().Where(e => !e.IsPassedEvent()).ToList();                        
           
            return Json(events, JsonRequestBehavior.AllowGet);
        }

        public ActionResult About()
        {            

            return View();
        }

        public ActionResult Contact()
        {
            
            return View();
        }

        protected override void Dispose(bool disposing)
        {            
            base.Dispose(disposing);
        }
    }
}
