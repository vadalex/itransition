using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Deck
    {
        public Deck()
        {
            Cards = new HashSet<Card>();
            DateCreated = DateTime.Now;
            IsShared = false;
        }

        public int DeckID { get; set; }

        [Required(ErrorMessage = "Title is required.")] 
        [MaxLength(80, ErrorMessage = "Title cannot be longer than 80 characters.")] 
        public string Title { get; set; }

        public string Description { get; set; }

        [Display(Name = "Date of creation")] 
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }

        [Display(Name = "Is shared?")] 
        public bool IsShared { get; set; }

        [Display(Name = "User")] 
        public int UserID { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Card> Cards { get; set; }
    }
}
