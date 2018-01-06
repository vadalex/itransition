using Contracts.BLL;
using Contracts.DAL;
using DAL;
using Entities;

namespace BusinessLogic.BLL
{
    public class CommentBL : GenericBusinessLogic<Comment>, ICommentBL
    {
        public CommentBL(MyContext context)
            : base(context)
        {
            //this.repository = DBFactoryInstance.GetFactoryInstance.CreateCustomerRepository(context);
        }

        public CommentBL(IRepository<Comment> repository)
            : base(repository)
        {
        }
    }
}
