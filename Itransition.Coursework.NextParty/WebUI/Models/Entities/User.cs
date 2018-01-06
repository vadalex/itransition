using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebUI.Models.Entities
{    
    public class User
    {
        public User()
        {
            Events = new HashSet<Event>();
            OrganizedEvents = new HashSet<Event>();
        }
        
        public int UserID { get; set; }
                
        public string UserName { get; set; }
                
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        public string Name { get; set; }
                
        public string Surname { get; set; }

        public virtual ICollection<Event> OrganizedEvents { get; set; }

        public virtual ICollection<Event> Events { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<DrinkAmount> DrinkAmounts { get; set; }
    }
}