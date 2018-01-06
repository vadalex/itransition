using System.Data.Entity;
using System.Linq;
using Contracts.DAL;

namespace DAL
{
    public class TodoRepository<T> : IRepository<T> where T : class
    {
        private readonly TodoContext context;
        private readonly DbSet<T> dbSet;

        public TodoRepository(TodoContext context)
        {
            this.context = context;
            this.dbSet = context.Set<T>();
        }

        public IQueryable<T> GetAll()
        {
            IQueryable<T> query = dbSet;
            return query;
        }

        public T GetSingle(int id)
        {
            return dbSet.Find(id);
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
            context.SaveChanges();
        }

        public void Remove(T entity)
        {
            context.Entry(entity).State = EntityState.Deleted;
            context.SaveChanges();
        }

        public void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}
