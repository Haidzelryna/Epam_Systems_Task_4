using System;
using System.ComponentModel.DataAnnotations;

namespace DAL
{
    /// <summary>
    /// Base class for all entities.
    /// </summary>
    public class Entity : IEntity
    {
        protected Entity()
        {
            Id = Guid.NewGuid();
        }

        [Key]
        [Required]
        public virtual Guid Id { get; set; }

        [Timestamp]
        public byte[] Version { get; set; }

        public bool IsNew()
        {
            return Version == null;
        }
    }
}
