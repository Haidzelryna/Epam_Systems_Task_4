using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain
{
    [Table("Contact")]
    public class Contact : BaseEntity
    {
        [Column("I")]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [Column("O")]
        [MaxLength(255)]
        public string MiddleName { get; set; }

        [Column("F")]
        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }

        [MaxLength(255)]
        public string Phone { get; set; }

        [MaxLength(255)]
        public string Email { get; set; }

        //[ForeignKey("UserId")]
        //public virtual ICollection<User> Users { get; set; }
    }
}

