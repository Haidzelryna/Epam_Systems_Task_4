using System;
using System.ComponentModel.DataAnnotations;

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

        [Key]
        [Required]
        public virtual Guid Id { get; set; }
    }
}
