using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MMSAPI.Repository
{
    public interface IBaseRepository<T>: IRepository where T : class
    {
        T GetById(string id);
        ICollection<T> GetAll();
        bool Add(T entity);
        bool Edit(T entity);
        bool Delete(string id);
        ICollection<T> Find(Expression<Func<T, bool>> expression);
    }
}
