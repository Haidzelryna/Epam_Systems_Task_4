﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
//using System.Threading;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T: Entity  
    {
        private DbContext _context;

        //static ReaderWriterLock rwl = new ReaderWriterLock();

        static object locker = new object();

        public GenericRepository()
        {
            _context = new SalesEntities();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public T Find(Guid Id)
        {
            //T result;
            //rwl.AcquireReaderLock(2000);
            lock (locker)
            {
                return _context.Set<T>().Find(Id);
            }
            //try
            //{
            //    result = _context.Set<T>().Find(Id);
            //}
            // }
            //finally
            //{
            //    rwl.ReleaseReaderLock();
            //}
            //return result;
        }

        public void Add(T entity)
        {
           _context.Set<T>().Add(entity);
        }

        public void Add(IEnumerable<T> entity)
        {
            _context.Set<T>().AddRange(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(IEnumerable<T> entity)
        {
            _context.Set<T>().RemoveRange(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
