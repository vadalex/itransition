using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entities;

namespace WebUI.Models
{
    public class StudyDeckModel
    {
        public UserStatistic Statistic { get; set; }
        public Deck Deck { get; set; }
        public Card Card { get; set; }
    }
}