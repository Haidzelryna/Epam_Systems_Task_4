using System.Linq;

namespace Domain.Repository
{
    public interface IGenericRepository<T> where T: Entity
    {
        //Get

        void Add(T entity);

        void Delete(T Entity);

        IQueryable<T> SaveChanges();
    }
}
