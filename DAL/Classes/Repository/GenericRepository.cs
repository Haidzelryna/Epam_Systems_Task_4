using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using DAL;

namespace DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T: Entity  
    {
        private DbContext _context;

        static object locker = new object();

        public GenericRepository(SalesEntities salesDbContext)
        {
            _context = salesDbContext;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public T Find(Guid Id)
        {
            lock (locker)
            {
                return _context.Set<T>().Find(Id);
            }
        }

        public async Task<T> FindAsync(Guid Id)
        {
            return await _context.Set<T>().FindAsync(Id);
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
