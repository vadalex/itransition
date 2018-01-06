using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DataRepository<T> : IRepository<T> where T : class
    {
        private FlashCardsContext context;
        private DbSet<T> dbSet;

        public DataRepository()
        {
            this.context = new FlashCardsContext();
            this.dbSet = context.Set<T>();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> where = null)
        {
            IQueryable<T> query = dbSet;
            if (where != null)
                query = query.Where(where);
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
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }
            context.Entry(entity).State = EntityState.Deleted;            
            context.SaveChanges();
        }

        public void Update(T entity)
        {            
            if (context.Entry(entity).State == EntityState.Detached)
            {
                dbSet.Attach(entity);
            }            
            context.Entry(entity).State = EntityState.Modified;            
            context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
