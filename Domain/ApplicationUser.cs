using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationUser : Entity, IBaseEntity
    {
        public ApplicationUser()
        {
            Id = Guid.NewGuid();
        }

        public Guid? ContactId { get; set; }

        //[ForeignKey(nameof(ContactId))]
        //public virtual Contact Contact { get; set; }

        public Guid? CreatedByUserId { get; set; }

        [ForeignKey(nameof(CreatedByUserId))]
        public ApplicationUser CreatedByUser { get; set; }

        [InverseProperty(nameof(CreatedByUser))]
        public virtual ICollection<ApplicationUser> NestedCreatedUsers { get; set; }

        public DateTime? CreatedDateTime { get; set; }

        public Guid? UpdatedByUserId { get; set; }

        [ForeignKey(nameof(UpdatedByUserId))]
        public ApplicationUser UpdatedByUser { get; set; }

        [InverseProperty(nameof(UpdatedByUser))]
        public virtual ICollection<ApplicationUser> NestedUpdatedUsers { get; set; }

        public DateTime? UpdatedDateTime { get; set; }

        /// <summary>
        /// This property is used by Entity Framework.
        /// </summary>
        [Timestamp]
        public byte[] Version { get; set; }

        public bool IsNew()
        {
            return Version == null;
        }
    }
}
