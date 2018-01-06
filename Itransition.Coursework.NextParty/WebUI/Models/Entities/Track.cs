using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models.Entities
{
    public class Track
    {
        public Track()
        {
            Events = new HashSet<Event>();
        }

        public int TrackID { get; set; }

        public string Title { get; set; }

        public string Album { get; set; }

        public string Artist { get; set; }

        [DataType(DataType.Url)]
        public string Link { get; set; }

        public virtual ICollection<Event> Events { get; set; }
    }
}