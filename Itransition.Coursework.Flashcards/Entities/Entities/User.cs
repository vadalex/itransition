using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    [Table("Users")]
    public class User
    {
        public User()
        {
            Decks = new HashSet<Deck>();
            UserStatistics = new HashSet<UserStatistic>();
        }

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int UserID { get; set; }

        [Display(Name = "Username")] 
        public string UserName { get; set; }
                
        [Display(Name = "Email address")]        
        public string Email { get; set; }
        
        public virtual ICollection<Deck> Decks { get; set; }
        public virtual ICollection<UserStatistic> UserStatistics { get; set; }
    }
}
