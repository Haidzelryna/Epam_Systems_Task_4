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
           _context.Set<Contact>().Add(entity as DAL.Contact);
        }

        public void Add(IEnumerable<T> entity)
        {
            _context.Set<Entity>().AddRange(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<Entity>().Add(entity);
        }

        public void Delete(IEnumerable<T> entity)
        {
            _context.Set<Entity>().RemoveRange(entity);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
