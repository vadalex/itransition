using BusinessLogic.BLL;
using Contracts.BLL;
using Contracts.DAL;
using Entities;

namespace BusinessLogic.TodoBLL
{
    public class TodoBL : GenericBusinessLogic<TodoItem>, IBusinessLogic<TodoItem>
    {
        public TodoBL(IRepository<TodoItem> repository)
            : base(repository)
        {
        }
    }
}
