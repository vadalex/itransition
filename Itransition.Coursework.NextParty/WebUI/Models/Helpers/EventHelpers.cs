using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebUI.Models.Entities;

namespace WebUI.Models.Helpers
{
    public static class EventHelpers
    {
        public static bool IsPassedEvent(this Event e)
        {
            if ((e.DatetimeOfStart + e.Duration) > DateTime.Now)
                return false;
            return true;
        }

        public static double GetAlcoholForParticipant(this Event e)
        {
            double sum = 0.0; 
            foreach(var amount in e.DrinkAmounts)
            { 
                sum += amount.BottleAmount * (amount.Drink.AlcoholByVolume * 0.01 * ((double)amount.Drink.Volume));
            }
            int partCount = e.Participants.Count;
            double res = sum / ((double)partCount);
            return res;
        }
    }
}