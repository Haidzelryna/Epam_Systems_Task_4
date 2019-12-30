using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Product : BaseEntity
    {
        [MaxLength(50)]
        public string Price { get; set; }
    }
}
