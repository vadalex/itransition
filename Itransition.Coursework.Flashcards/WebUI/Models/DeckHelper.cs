using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;
using DataAccessLayer;

namespace WebUI.Models
{
    public static class DeckHelper
    {
        public static void UpdateDeckNumbers(int deckId, IRepository<Deck> rep)
        {            
            var deck = rep.GetSingle(deckId);
            if(deck == null)
                return;
            var cards = deck.Cards.OrderBy(c => c.Number);
            for (int i = 1; i <= deck.Cards.Count(); i++)
            {
                cards.Skip(i - 1).FirstOrDefault().Number = i;
            }
            rep.Update(deck);
        }
    }
}