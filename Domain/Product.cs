using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BLL
{
    [Table("Product")]
    public class Product : Entity
    {
        [MaxLength(50)]
        public string Price { get; set; }
    }
}
