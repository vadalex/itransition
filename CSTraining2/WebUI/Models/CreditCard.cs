using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CSTraining.WebUI.Models
{
    public class CreditCard
    {
        public Guid CreditCardId { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        public decimal Balance { get; set; }
    }
}