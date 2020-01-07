using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T: Entity  
    {
        private DbContext _context;

        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
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
