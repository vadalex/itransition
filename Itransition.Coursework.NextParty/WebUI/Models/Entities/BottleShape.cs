using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models.Entities
{
    public class BottleShape
    {
        public BottleShape()
        {
            Drinks = new HashSet<Drink>();
        }

        public int BottleShapeID { get; set; }
                
        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(40, ErrorMessage = "Title cannot be longer than 40 characters")] 
        public string Title { get; set; }

        public virtual ICollection<Drink> Drinks { get; set; }
    }
}