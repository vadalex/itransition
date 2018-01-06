using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcApp.Models
{
    public class PaymentModel
    {
        public string CallbackUrl { get; set; }
        public string ReturnSuccessUrl { get; set; }
        public Order Order { get; set; }
        public string BuyerName { get; set; }
    }
}
