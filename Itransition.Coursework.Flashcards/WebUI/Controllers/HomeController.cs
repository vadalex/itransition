using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Entities;
using DataAccessLayer;
using System.Data;
using WebUI.Filters;

namespace WebUI.Controllers
{
    [InitializeSimpleMembership]
    public class HomeController : Controller
    {
        private IRepository<Deck> deckRepository;

        public HomeController(IRepository<Deck> repository)
        {
            this.deckRepository = repository;
        }
        
        public ActionResult Index()
        {
            /*
            using (var context = new FlashCardsContext())
            {               

                var decks = new List<Deck>() {
                new Deck() { UserID = context.Users.First().UserID, Title="Russian Words", Description="some words about this deck", IsShared = true},                
                new Deck() { UserID = context.Users.First().UserID, Title="Top 100 German Words", Description="some words about this deck", IsShared = true },
                };
                decks.ForEach(d=>context.Decks.Add(d));
                context.SaveChanges();

                var cards = new List<Card>() {
                new Card() { DeckID = context.Decks.First().DeckID, Question="большое спасибо", Answer="thank you very much", Number=1 },
                new Card() { DeckID = context.Decks.First().DeckID, Question="я думаю", Answer="I think", Number=2 },
                new Card() { DeckID = context.Decks.First().DeckID, Question="во-первых", Answer="in the first place", Number=3 },
                new Card() { DeckID = context.Decks.First().DeckID, Question="быть", Answer="to be", Number=4 },
                new Card() { DeckID = context.Decks.First().DeckID, Question="я говорю по русски", Answer="I speak russian", Number=5 },
                new Card() { DeckID = context.Decks.First().DeckID, Question="холод", Answer="cold", Number=6 },

                new Card() { DeckID = context.Decks.OrderBy(i => i.DeckID).Skip(1).First().DeckID, Question="wollen", Answer="to want", QuestionSubTitle="(verb)", Number=1 },
                new Card() { DeckID = context.Decks.OrderBy(i => i.DeckID).Skip(1).First().DeckID, Question="mich", Answer="me", QuestionSubTitle="(pron.)", Number=2 },
                new Card() { DeckID = context.Decks.OrderBy(i => i.DeckID).Skip(1).First().DeckID, Question="immer", Answer="always", QuestionSubTitle="(adv.)", Number=3 },
                new Card() { DeckID = context.Decks.OrderBy(i => i.DeckID).Skip(1).First().DeckID, Question="sehr", Answer="very", QuestionSubTitle="(adv.)", Number=4 }                
                };
                cards.ForEach(c=>context.Cards.Add(c));                                
                context.SaveChanges();

            } 
            */
               /*        
            var context1 = new FlashCardsContext();
            User user = new User { UserName = "jack", Email = "email@email.by" };
            context1.Users.Add(user);
            context1.SaveChanges();
             */
            /*
            UserStatistic us = new UserStatistic() { UserID = context1.Users.First().UserID, DeckID = 1, UnknownCards = context1.Decks.First().Cards.OrderBy(c=>c.CardID).Skip(3).ToList() };
            var rep1 = new DataRepository<UserStatistic>(context1);
            rep1.Add(us);            
            var list1 = rep1.GetAll().First().UnknownCards;           
            */            
            var deckList = deckRepository.GetAll(d => d.IsShared).OrderByDescending(d => d.DateCreated);                        
            return View(deckList);
        }

        public ActionResult Details(int id = 0)
        {
            Deck deck = deckRepository.GetSingle(id);
            if (deck == null)            
                return HttpNotFound();            
            return View(deck);
        }

        public ActionResult About()
        {
            return View();
        }
        
        protected override void Dispose(bool disposing)
        {
            deckRepository.Dispose();            
            base.Dispose(disposing);
        }

    }
}
