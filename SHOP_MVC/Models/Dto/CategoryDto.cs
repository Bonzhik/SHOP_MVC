using System.ComponentModel.DataAnnotations;

namespace SHOP_MVC.Models.Dto
{
    public class CategoryDto
    {
        [Required]
        public int Id { get; set; } = 0;
        [Required]
        [StringLength(12)]
        public string Title { get; set; }
    }
}
