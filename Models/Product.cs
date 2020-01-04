using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models
{
    [Table("Product")]
    public class Product : Entity
    {
        [MaxLength(50)]
        public string Price { get; set; }
    }
}
