using System;

namespace Entities
{
    public class Comment
    {
        public int CommentID { get; set; }

        public DateTime CreatedDate { get; set; }
        
        public string Text { get; set; }

        public int UserID { get; set; }
    }
}
