using System.Linq;

namespace CSTraining.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        T GetSingle(int id);
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);
    }
}
