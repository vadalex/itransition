using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebMatrix.WebData;
using DataAccessLayer;
using Entities;
using WebUI.Models;
using WebUI.Filters;

namespace WebUI.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class MyDeckController : Controller
    {
        private IRepository<Deck> deckRepository;

        public MyDeckController(IRepository<Deck> repository)
        {
            this.deckRepository = repository;
        }
                

        public ActionResult Index()
        {            
            var userId = WebSecurity.CurrentUserId;
            var decks = deckRepository.GetAll(d => d.UserID == userId).OrderByDescending(d => d.DateCreated);
            return View(decks);
        }


        public ActionResult Details(int deckId = 0)
        {
            Deck deck = deckRepository.GetSingle(deckId);
            if (deck == null)
            {
                return HttpNotFound();
            }
            return View(deck);
        }        

        public ActionResult Create()
        {            
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Deck deck)
        {
            if (ModelState.IsValid)
            {
                deck.DateCreated = DateTime.Now;
                deck.UserID = WebSecurity.CurrentUserId;
                deckRepository.Add(deck);                
                return RedirectToAction("Index");
            }        
            return View(deck);
        }
       
        public ActionResult Edit(int deckId = 0)
        {
            Deck deck = deckRepository.GetSingle(deckId);
            if (deck == null)
            {
                return HttpNotFound();
            }            
            return View(deck);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Deck deck)
        {
            if (ModelState.IsValid)
            {
                deckRepository.Update(deck);                
                return RedirectToAction("Index");
            }            
            return View(deck);
        }

        public ActionResult Delete(int deckId = 0)
        {
            Deck deck = deckRepository.GetSingle(deckId);
            if (deck == null)
            {
                return HttpNotFound();
            }
            return View(deck);
        }
                
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int DeckID)
        {
            Deck deck = deckRepository.GetSingle(DeckID);
            deckRepository.Remove(deck);            
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            deckRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}