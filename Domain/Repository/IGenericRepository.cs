using System;
using System.Data.Entity;

namespace BLL.Repository
{
    public interface IGenericRepository<T> where T: Entity
    {
        Guid Get(DbSet dbSet, Guid Id);

        void Add(T entity);

        void Delete(T Entity);

        //IQueryable<T> SaveChanges();
    }
}
