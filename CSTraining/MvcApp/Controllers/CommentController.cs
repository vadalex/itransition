using System;
using System.Linq;
using System.Web.Mvc;
using Contracts.BLL;
using Entities;
using Microsoft.Ajax.Utilities;
using WebMatrix.WebData;

namespace MvcApp.Controllers
{
    [Authorize]
    public class CommentController : BaseController
    {
        public CommentController(IBusinessLogicLayer bll) : base(bll) { }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PostComment(string comment, int userId)
        {
            if (comment.IsNullOrWhiteSpace())
                return Json((object) false);
            Comment c = new Comment();
            c.CreatedDate = DateTime.Now;
            c.Text = comment;
            c.UserID = userId;
            bll.Comments.Add(c);
            return Json((object)true);
        }

        [HttpPost]
        public ActionResult GetAllComments()
        {
            var comments =
                bll.Comments.GetAll()
                    .OrderByDescending(c => c.CreatedDate)
                    .Select(
                        c =>
                            new
                            {
                                Author = bll.Users.GetSingle(c.UserID).UserName,
                                Date = c.CreatedDate.ToString("dd MMMM yyyy hh:mm tt"),
                                Comment = c.Text
                            });
            return Json(comments);
        }
    }
}