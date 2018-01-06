using System.Linq;
using System.Web.Mvc;
using BusinessLogic.TodoBLL;
using DAL;
using Entities;
using Contracts.BLL;
using Contracts.DAL;

namespace TodoService.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //var repository = DependencyResolver.Current.GetService(typeof(IRepository<TodoItem>)) as IRepository<TodoItem>;
            //IRepository<TodoItem> r = new TodoRepository<TodoItem>();
            //IBusinessLogic<TodoItem> b = new TodoBL(repository);
            //var t1 = b.GetAll().FirstOrDefault().TodoText;
            return View();
        }
    }
}
