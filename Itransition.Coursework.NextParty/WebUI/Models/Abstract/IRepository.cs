﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace WebUI.Models.Abstract
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IQueryable<T> GetAll(Expression<Func<T, bool>> where = null);
        T GetSingle(int id);
        void Add(T entity);
        void Remove(T entity);
        void Update(T entity);
    }
}
