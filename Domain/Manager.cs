using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Manager")]
    public class Manager : Entity
    {
        public Guid? ContactId { get; set; }
        //[ForeignKey(nameof(ContactId))]
        //public virtual Contact Contact { get; set; }
    }
}
