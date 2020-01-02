using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    /// <summary>
    /// 
    /// </summary>
    [Table("User")]
    public class User : BaseEntity
    {
        public Guid? ContactId { get; set; }
        //[ForeignKey(nameof(ContactId))]
        //public virtual Contact Contact { get; set; }
    }
}
