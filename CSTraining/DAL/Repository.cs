using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts.DAL;
using Unity;
using Microsoft.Practices.Unity;

namespace DAL
{
    public class DataRepository<T> : IRepository<T> where T : class
    {
        private MyContext context;
        private DbSet<T> dbSet;

        public DataRepository(MyContext context)
        {            
            this.context = context;            
            this.dbSet = context.Set<T>();
        }

        public DataRepository()
        {
            this.context = UnityContainerHolder.GetContainer.Resolve<MyContext>();
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
