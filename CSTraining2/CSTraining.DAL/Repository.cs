using CSTraining.DAL.Interfaces;
using System.Data.Entity;
using System.Linq;

namespace CSTraining.DAL
{
    public class TodoRepository<T> : IRepository<T> where T : class
    {
        private readonly CSTrainingContext context;
        private readonly DbSet<T> dbSet;

        public TodoRepository(CSTrainingContext context)
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
