using System.Linq;

namespace Domain.Repository
{
    public interface IGenericRepository<IEntityRepos>
    {
        //Get

        void Add(IEntityRepos entity);

        void Delete(IEntityRepos Entity);

        IQueryable<IEntityRepos> SaveChanges();
    }
}
