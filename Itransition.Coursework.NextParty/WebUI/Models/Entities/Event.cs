using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models.Entities
{
    public class Event
    {
        public Event()
        {
            Participants = new HashSet<User>();
            Tracks = new HashSet<Track>();
            Comments = new HashSet<Comment>();
            Tags = new HashSet<Tag>();
            DrinkAmounts = new HashSet<DrinkAmount>();
            Duration = new TimeSpan(2, 0, 0);
            DatetimeOfStart = DateTime.Now;
        }

        public int EventID { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        [MaxLength(100, ErrorMessage = "Title cannot be longer than 100 characters")] 
        public string Title { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:d MMMM yyyy, hh.mm tt}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date and time of start")]
        public DateTime DatetimeOfStart { get; set; }

        [DataType(DataType.Duration)]        
        public TimeSpan Duration { get; set; }
                
        public string Address { get; set; }
                
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int OrganizerID { get; set; }

        public virtual User Organizer { get; set; }

        public virtual ICollection<User> Participants { get; set; }

        public virtual ICollection<Track> Tracks { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<DrinkAmount> DrinkAmounts { get; set; }

        public virtual ICollection<Tag> Tags { get; set; }
    }
}