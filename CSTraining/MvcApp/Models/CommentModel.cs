using Entities;

namespace MvcApp.Models
{
    public class CommentModel
    {
        public Comment Comment { get; set; }
        public User User { get; set; }
    }
}
