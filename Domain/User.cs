using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    public class User : BaseEntity
    {
        public Guid? ContactId { get; set; }
        //[ForeignKey(nameof(ContactId))]
        //public virtual Contact Contact { get; set; }
    }
}
