using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models.Entities
{
    public class DrinkAmount
    {
        public int DrinkAmountID { get; set; }
                
        [DisplayFormat(DataFormatString = "x{0:0.#}", ApplyFormatInEditMode = true)]
        [Range(0.0, double.MaxValue, ErrorMessage = "Incorrect ABV")]
        public double BottleAmount { get; set; }

        public int DrinkID { get; set; }

        public int UserID { get; set; }

        public int EventID { get; set; }

        public virtual Drink Drink { get; set; }

        public virtual User User { get; set; }

        public virtual Event Event { get; set; }

    }
}