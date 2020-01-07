using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL
{
    /// <summary>
    /// 
    /// </summary>
    [Table("Manager")]
    public class Manager : Entity
    {
        public Guid? ContactId { get; set; }
        [ForeignKey(nameof(ContactId))]
        public virtual Contact Contact { get; set; }
    }
}
