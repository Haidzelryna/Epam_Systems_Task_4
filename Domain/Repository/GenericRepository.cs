using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repository
{
    public class GenericRepository<IEntityRepos> : IGenericRepository<IEntityRepos>     
    {
        public void Add(IEntityRepos entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(IEntityRepos entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<IEntityRepos> SaveChanges()
        {
            throw new NotImplementedException();
        }
    }
}
