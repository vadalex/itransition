using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Entities;

namespace Contracts.BLL
{
    public interface IBusinessLogic<T> where T : class
    {
        List<T> GetAll(Expression<Func<T, bool>> predicate = null);
        T GetSingle(int id);
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);
    }
}