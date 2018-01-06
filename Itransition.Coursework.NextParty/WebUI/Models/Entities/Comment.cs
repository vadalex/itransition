using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebUI.Models.Entities
{
    public class Comment
    {
        public Comment()
        {
            DateCreated = DateTime.Now;
        }

        public int CommentID { get; set; }

        [DataType(DataType.MultilineText)]
        public string MarkdownText { get; set; }

        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:ddd, d MMMM yyyy, hh.mm tt}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }

        public int EventID { get; set; }

        public int UserID { get; set; }

        public virtual Event Event { get; set; }

        public virtual User User { get; set; }

        
    }
}