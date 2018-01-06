using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class Card
    {
        public Card()
        {
            UserStatistics = new HashSet<UserStatistic>();
        }

        public int CardID { get; set; }

        [MaxLength(150, ErrorMessage = "Question cannot be longer than 150 characters.")] 
        public string Question { get; set; }

        [MaxLength(100, ErrorMessage = "Question cannot be longer than 100 characters.")] 
        public string Answer { get; set; }

        [Display(Name="Question subtitle")]
        [MaxLength(100, ErrorMessage = "Subtitle cannot be longer than 100 characters.")] 
        public string QuestionSubTitle { get; set; }

        [Display(Name = "Answer subtitle")]
        [MaxLength(100, ErrorMessage = "Subtitle cannot be longer than 100 characters.")] 
        public string AnswerSubTitle { get; set; }

        [Display(Name = "Question image")]
        public string QuestionImageLink { get; set; }

        [Display(Name = "Answer image")]
        public string AnswerImageLink { get; set; }

        public int Number { get; set; }

        [Display(Name = "Deck")] 
        public int DeckID { get; set; }                
        public virtual Deck Deck { get; set; }
        public virtual ICollection<UserStatistic> UserStatistics { get; set; }
    }
}
