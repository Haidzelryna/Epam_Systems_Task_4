using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public interface IGenericRepository<T> where T: Entity
    {
        Task<IEnumerable<T>> GetAllAsync();

        T Find(Guid Id);

        void Add(T entity);

        void Add(IEnumerable<T> entity);

        void Delete(T Entity);

        void Delete(IEnumerable<T> entity);

        void SaveChanges();
    }
}
