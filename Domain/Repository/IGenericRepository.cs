using System.Linq;

namespace Domain.Repository
{
    public interface IGenericRepository<Entity, IEntity>: IEntity where Entity: IEntity
    {
        //Get

        void Add(IEntity entity);

        void Delete(IEntity Entity);

        IQueryable<IEntity> SaveChanges();
    }
}
