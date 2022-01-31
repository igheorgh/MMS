﻿using DataLibrary;
using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MMSAPI.Repository
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        internal MMSContext _context;
        public BaseRepository(MMSContext context)
        {
            this._context = context;
        }

        public bool Add(T entity)
        {
            try
            {
                _context.Set<T>().Add(entity);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }
        }

            public bool Delete(string id)
        {
                try
                {
                    var obj = _context.Set<T>().Find(id);
                    if (obj != null)
                    {
                        _context.Remove(obj);
                        _context.SaveChanges();
                        return true;
                    }
                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

        public virtual bool Edit(T entity)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public ICollection<T> Find(Expression<Func<T, bool>> expression)
        {
            return _context.Set<T>().Where(expression).ToList();
        }

        public ICollection<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T GetById(string id)
        {
            return _context.Set<T>().Find(id);
        }

    }
}
