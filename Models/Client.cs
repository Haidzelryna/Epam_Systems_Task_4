using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Client")]
    public class Client : Entity
    {
        public Guid? ContactId { get; set; }
        [ForeignKey(nameof(ContactId))]
        public virtual Contact Contact { get; set; }
    }
}
