using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class GenericRepository<Entity, IEntity> : IGenericRepository<Entity, IEntity> //where Entity : class, interface        
    {
        public void Add(IEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(IEntity entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<IEntity> SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
