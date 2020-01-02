using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class BaseEntity : Entity
    {
        [Required]
        public Guid? CreatedByUserId { get; set; }

        //[ForeignKey(nameof(CreatedByUserId))]
        //public virtual User CreatedByUser { get; set; }

        [Required]
        public DateTime? CreatedDateTime { get; set; }

        public Guid? UpdatedByUserId { get; set; }

        //[ForeignKey(nameof(UpdatedByUserId))]
        //public virtual User UpdatedByUser { get; set; }

        public DateTime? UpdatedDateTime { get; set; }
    }
}
