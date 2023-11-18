using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SHOP_MVC.Models.Dto
{
    public class ProductDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(12)]
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public ICollection<Category> Categories { get; set; }
        [Required]
        [Range(0,10000)]
        public int Quantity { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
    }
}
