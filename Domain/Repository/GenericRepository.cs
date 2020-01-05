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
        private DbContext _context;

        public GenericRepository(DbContext context)
        {
            _context = context;
        }

        public Guid? Get(Guid Id)
        {
            var manager = ((SalesEntities)_context).Manager.Find(Id);
            //if (manager == null) { return null; }
            return manager.Id;
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
