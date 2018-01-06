using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities;
using DataAccessLayer;
using WebMatrix.WebData;
using WebUI.Models;
using WebUI.Filters;

namespace WebUI.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class CardController : Controller
    {
        private IRepository<Card> cardRepository;
        private IRepository<Deck> deckRepository;

        public CardController(IRepository<Card> cardRep, IRepository<Deck> deckRep)
        {
            this.cardRepository = cardRep;
            this.deckRepository = deckRep;
        }        
        
        public ActionResult Details(int cardId = 0)
        {
            Card card = cardRepository.GetSingle(cardId);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        public ActionResult Create(int deckId = 0)
        {
            Deck deck = deckRepository.GetSingle(deckId);
            if (deck == null)
            {
                return HttpNotFound();
            }
            ViewBag.deckId = deckId;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Card card, HttpPostedFileBase questionImage = null, HttpPostedFileBase answerImage = null)
        {
            if (ModelState.IsValid)
            {
                var imageCloud = new ImageCloud();
                if(questionImage != null)
                    card.QuestionImageLink = imageCloud.ImageUpload(questionImage);
                if (answerImage != null)
                    card.AnswerImageLink = imageCloud.ImageUpload(answerImage);
                var list = deckRepository.GetSingle(card.DeckID).Cards;
                card.Number = list.Count() == 0  ? 1 : list.OrderBy(c => c.Number).Last().Number + 1;
                cardRepository.Add(card);
                return RedirectToAction("Details", "MyDeck", new { deckId = card.DeckID });
            }
            return View(card);
        }

        public ActionResult Edit(int cardId = 0)
        {
            Card card = cardRepository.GetSingle(cardId);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Card card, HttpPostedFileBase questionImage = null, HttpPostedFileBase answerImage = null, 
            bool deleteQuestionImage = false, bool deleteAnswerImage = false)
        {
            if (ModelState.IsValid)
            {
                var imageCloud = new ImageCloud();
                if (!deleteQuestionImage)
                {
                    if (questionImage != null)
                        card.QuestionImageLink = imageCloud.ImageUpload(questionImage);
                }
                else
                {
                    card.QuestionImageLink = null;
                }
                if (!deleteAnswerImage)
                {
                    if (answerImage != null)
                        card.AnswerImageLink = imageCloud.ImageUpload(answerImage);
                }
                else
                {
                    card.AnswerImageLink = null;
                }

                cardRepository.Update(card);
                return RedirectToAction("Details", "MyDeck", new { deckId = card.DeckID });
            }
            return View(card);
        }

        public ActionResult Delete(int cardId = 0)
        {
            Card card = cardRepository.GetSingle(cardId);
            if (card == null)
            {
                return HttpNotFound();
            }
            return View(card);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int cardId)
        {
            Card card = cardRepository.GetSingle(cardId);
            var deckId = card.DeckID;
            cardRepository.Remove(card);
            DeckHelper.UpdateDeckNumbers(deckId, deckRepository);
            return RedirectToAction("Details", "MyDeck", new { deckId = deckId });
        }

        protected override void Dispose(bool disposing)
        {
            deckRepository.Dispose();
            cardRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}