using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class UserStatistic
    {
        public UserStatistic()
        {
            UnknownCards = new HashSet<Card>();
        }

        public int UserStatisticID { get; set; }

        [Display(Name = "Deck")] 
        public int DeckID { get; set; }

        [Display(Name = "Unknown cards")]
        public virtual ICollection<Card> UnknownCards { get; set; }

        [Display(Name = "Last study date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime LastViewed { get; set; }

        [Display(Name = "User")] 
        public int UserID { get; set; }
        public virtual User User { get; set; }
    }
}
