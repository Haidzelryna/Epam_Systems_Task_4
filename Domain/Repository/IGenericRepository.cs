using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Repository
{
    public interface IGenericRepository<T> where T: Entity
    {
        Task<IEnumerable<T>> Get();

        Guid Find(DbSet dbSet, Guid Id);

        void Add(T entity);

        void Delete(T Entity);

        //IQueryable<T> SaveChanges();
    }
}
