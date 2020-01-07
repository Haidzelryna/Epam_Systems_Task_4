using System;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IGenericRepository<T> where T: Entity
    {
        Task<IEnumerable<T>> GetAllAsync();

        Guid Find(DbSet dbSet, Guid Id);

        void Add(T entity);

        void Add(IEnumerable<T> entity);

        void Delete(T Entity);

        void Delete(IEnumerable<T> entity);

        void SaveChanges();
    }
}
