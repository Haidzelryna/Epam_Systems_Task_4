using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DAL
{
    [Table("Product")]
    public class Product : Entity
    {
        [MaxLength(50)]
        public string Price { get; set; }
    }
}
