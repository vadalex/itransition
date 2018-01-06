using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebUI.Models.Entities;

namespace WebUI.Models
{
    public class IndexViewModel
    {
        public List<Event> BestEvents { get; set; }
        public List<Event> WorstEvents { get; set; }
        public List<Event> AllFutureEvents { get; set; }
        
    }
}