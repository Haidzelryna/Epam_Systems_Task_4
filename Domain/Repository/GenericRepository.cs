using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Linq;

namespace Domain.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T: Entity    
    {
        private DataContext _context;

        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
           _context.Set<Entity>().Add(entity as Entity);
        }

        public void Delete(T entity)
        {
            _context.Set<Entity>().Add(entity as Entity);
        }

        public IQueryable<T> SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
