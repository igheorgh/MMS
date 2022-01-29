using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MMSAPI.Repository
{
    public interface IBaseRepository<T> where T : class
    {
        T GetById(int id);
        IEnumerable<T> GetAll();
        bool Add(T entity);
        bool Edit(T entity);
        bool Delete(int id);
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    }
}
