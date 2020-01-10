using System;

namespace BLL
{
    /// <summary>
    /// Base class for all entities.
    /// </summary>
    public class Entity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        public virtual Guid Id { get; set; }
    }
}
