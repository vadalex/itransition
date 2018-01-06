using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Linq.Expressions;
using Contracts.DAL;
using Contracts.BLL;

namespace BusinessLogic.BLL
{
    public abstract class GenericBusinessLogic<T> : IBusinessLogic<T> where T: class 
    {       
        protected IRepository<T> repository;
     
        public GenericBusinessLogic(MyContext context) { }

        public GenericBusinessLogic(IRepository<T> repository)
        {
            this.repository = repository;
        }

        public virtual List<T> GetAll(Expression<Func<T, bool>> predicate = null)
        {
            var result = repository.GetAll();
            if (predicate != null)
                result = result.Where(predicate);
            return result.ToList();
        }

        public virtual T GetSingle(int id)
        {
            return repository.GetSingle(id);
        }

        public virtual void Add(T entity)
        {
            repository.Add(entity);            
        }

        public virtual void Remove(T entity)
        {
            repository.Remove(entity);            
        }

        public virtual void Update(T entity)
        {
            repository.Update(entity);            
        }

    }
}
