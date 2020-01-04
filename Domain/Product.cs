using System.ComponentModel.DataAnnotations;

namespace Domain
{
    //[Table("Product")]
    public class Product : Entity
    {
        [MaxLength(50)]
        public string Price { get; set; }
    }
}
