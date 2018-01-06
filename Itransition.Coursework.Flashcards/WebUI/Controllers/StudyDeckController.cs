using System;
using System.Collections.Generic;
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
    [InitializeSimpleMembership]
    public class StudyDeckController : Controller
    {
        private IRepository<UserStatistic> statisticRepository;
        private IRepository<Deck> deckRepository;
        
        public StudyDeckController(IRepository<UserStatistic> statistics, IRepository<Deck> decks)
        {
            this.statisticRepository = statistics;
            this.deckRepository = decks;
        }

        public ActionResult Start(int deckId = 0)
        {
            if (deckId == 0)            
                return HttpNotFound();           
            UserStatistic statistic;
            if (WebSecurity.IsAuthenticated)
            {
                statistic = statisticRepository.GetAll(s => s.UserID == WebSecurity.CurrentUserId && s.DeckID == deckId).FirstOrDefault();
                if (statistic == null)
                {
                    statistic = new UserStatistic() { UserID = WebSecurity.CurrentUserId, DeckID = deckId, LastViewed = DateTime.Now };
                    statisticRepository.Add(statistic);
                }
                else
                {
                    statistic.LastViewed = DateTime.Now;
                    statistic.UnknownCards.Clear();
                    statisticRepository.Update(statistic);
                }
            }
            else
            {
                statistic = new UserStatistic() { UserID = -1, DeckID = deckId, LastViewed = DateTime.Now, UnknownCards = new HashSet<Card>() };
            }
            Session["statistic"] = statistic;

            return RedirectToAction("ShowNextCard", new { deckId = deckId });
        }        

        public ActionResult Question(int deckId, int cardId, UserStatistic statistic)
        {               
            var deck = deckRepository.GetSingle(deckId);
            var model = new StudyDeckModel()
            {
                Deck = deck,
                Card = deck.Cards.Where(c => c.CardID == cardId).FirstOrDefault(),
                Statistic = statistic
            };
            return View(model);
        }

        public ActionResult CorrectCard(int deckId, int cardId)
        {
            return RedirectToAction("ShowNextCard", new { deckId = deckId, cardId = cardId });
        }

        public ActionResult IncorrectCard(int deckId, int cardId, UserStatistic statistic)
        {
            var crd = deckRepository.GetSingle(deckId).Cards.Where(c => c.CardID == cardId).FirstOrDefault();            
            statistic.UnknownCards.Add(crd);
            if (statistic.UserStatisticID > 0)            
            {
                var stat = statisticRepository.GetSingle(statistic.UserStatisticID);
                var card = stat.User.Decks.Where(d => d.DeckID == deckId).FirstOrDefault().Cards.Where(c => c.CardID == cardId).FirstOrDefault();
                stat.UnknownCards.Add(card);
                statisticRepository.Update(stat);
            }
            return RedirectToAction("ShowNextCard", new { deckId = deckId, cardId = cardId });
        }

        public ActionResult ShowNextCard(int deckId = 0, int cardId = 0)
        {            
            var cards = deckRepository.GetSingle(deckId).Cards.OrderBy(c => c.Number);
            if(cards.Count() == 0)
                return RedirectToAction("Finish", new { deckId = deckId });
            int nextCardId = cards.FirstOrDefault().CardID;
            if(cardId != 0)
            {
                if (cards.Last().CardID == cardId)
                {
                    return RedirectToAction("Finish", new { deckId = deckId });
                }            
                Card curCard = deckRepository.GetSingle(deckId).Cards.Where(c => c.CardID == cardId).FirstOrDefault();
                nextCardId = cards.SkipWhile(c => c.Number <= curCard.Number).FirstOrDefault().CardID;
            }
            return RedirectToAction("Question", new { deckId = deckId, cardId = nextCardId });
        }

        public ActionResult Finish(int deckId = 0)
        {
            if (deckId == 0)
                return HttpNotFound();   
            var model = new StudyDeckModel()
            {
                Deck = deckRepository.GetSingle(deckId),
                Card = null,
                Statistic = (UserStatistic)Session["statistic"]
            };
            return View(model);
        }
        protected override void Dispose(bool disposing)
        {
            deckRepository.Dispose();
            statisticRepository.Dispose();
            base.Dispose(disposing);
        }
    }
}
