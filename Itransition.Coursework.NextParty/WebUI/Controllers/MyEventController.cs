using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models.Entities;
using WebUI.Models.DataAccessLayer;
using WebUI.Models.Abstract;
using WebMatrix.WebData;
using WebUI.Models.Helpers;

namespace WebUI.Controllers
{
    [Authorize]
    public class MyEventController : Controller
    {
        private DataRepositories rep;

        public MyEventController()
        {
            this.rep = new DataRepositories();
        }
              
        public ActionResult OrganizedEvents()
        {
            var userId = WebSecurity.CurrentUserId;
            var events = rep.Users.GetSingle(userId).OrganizedEvents.OrderByDescending(e=>e.DatetimeOfStart);
            return View(events);
        }

        public ActionResult MyEvents()
        {
            var userId = WebSecurity.CurrentUserId;
            var events = rep.Users.GetSingle(userId).Events.OrderByDescending(e => e.DatetimeOfStart);
            return View(events);
        }
        
        public ActionResult Details(int eventId = 0)
        {
            Event ev = rep.Events.GetSingle(eventId);
            if (ev == null)
            {
                return HttpNotFound();
            }
            if (WebSecurity.CurrentUserId == ev.OrganizerID)
                return View("MyEventDetails", ev);
            return View(ev);
        }

        //
        // GET: /MyEvent/Create

        public ActionResult Create()
        {            
            return View(new WebUI.Models.Entities.Event());
        }

        //
        // POST: /MyEvent/Create

        [HttpPost]        
        public ActionResult Create(Event ev, string eventTags)
        {
            if (ModelState.IsValid)
            {                
                var tags = eventTags.Split(' ', ',');
                var alltags = rep.Tags.GetAll();
                foreach (var tag in tags)
                {
                    if (!alltags.Any(t => t.Value == tag))
                        rep.Tags.Add(new Tag() { Value = tag });
                }
                alltags = rep.Tags.GetAll(t=>tags.Any(s=>s == t.Value));
                ev.Tags = alltags.ToList();
                var user = rep.Users.GetSingle(WebSecurity.CurrentUserId);
                ev.Organizer = user;
                ev.Participants.Add(user);              
                rep.Events.Add(ev);
                return RedirectToAction("OrganizedEvents");
            }                       
            return View(ev);
        }

        //
        // GET: /MyEvent/Edit/5

        public ActionResult Edit(int eventId = 0)
        {
            Event ev = rep.Events.GetSingle(eventId);
            if (ev == null)
            {
                return HttpNotFound();
            }            
            return View(ev);
        }

        //
        // POST: /MyEvent/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Event ev)
        {
            if (ModelState.IsValid)
            {
                rep.Events.Update(ev);
                return RedirectToAction("Index");
            }            
            return View(ev);
        }

        //
        // GET: /MyEvent/Delete/5

        public ActionResult Delete(int eventId = 0)
        {
            Event ev = rep.Events.GetSingle(eventId);
            if (ev == null)
            {
                return HttpNotFound();
            }
            return View(ev);
        }

        //
        // POST: /MyEvent/Delete/5

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int eventId)
        {
            var ev = rep.Events.GetSingle(eventId);
            rep.Events.Remove(ev);
            return RedirectToAction("Index");
        }
        
        protected override void Dispose(bool disposing)
        {
            rep.Dispose();
            base.Dispose(disposing);
        }
    }
}