using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models.Entities
{
    public class Tag
    {
        public Tag()
        {
            Events = new HashSet<Event>();
        }

        public int TagID { get; set; }
                
        public string Value { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}