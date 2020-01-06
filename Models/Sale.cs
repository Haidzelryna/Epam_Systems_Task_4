using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL
{
    [Table("Sale")]
    public class Sale : BaseEntity
    {
        public DateTime? Date { get; set; }

        public Guid? ClientId { get; set; }
        [ForeignKey("ClientId")]
        public virtual ICollection<Client> Clients { get; set; }

        public Guid? ProductId { get; set; }
        [ForeignKey("ProductId")]
        public virtual ICollection<Product> Products { get; set; }

        [MaxLength(50)]
        public string Sum { get; set; }
    }
}
