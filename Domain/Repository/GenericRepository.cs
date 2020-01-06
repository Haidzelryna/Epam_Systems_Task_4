using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace BLL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T: Entity    
    {
        private DbContext _context;

        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        public IEnumerable<T> Get()
        {
            return _context.
        }

        public Guid Find(DbSet dbSet, Guid Id)
        {
            var entity = dbSet.Find(Id);
            return ((Entity)entity).Id;
        }

        public void Add(T entity)
        {
           _context.Set<Entity>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<Entity>().Add(entity);
        }

        //public IQueryable<T> SaveChanges()
        //{
        //    // _context.SaveChanges();
        //}
    }
}
