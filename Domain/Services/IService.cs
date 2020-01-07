using System.Collections.Generic;

namespace BLL.Services
{
    public interface IService<T,V>
    {
        T Get(V Entity);

        IEnumerable<T> Get(IEnumerable<V> Entities);

        void Add(T Entity);

        void Add(IEnumerable<T> Entities);

        void Remove(T Entity);

        void Remove(IEnumerable<T> Entities);

        void SaveChanges();
    }
}
