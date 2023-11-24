using System.ComponentModel.DataAnnotations;

namespace SHOP_MVC.Models.Dto
{
    public class CategoryDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string Title { get; set; }
    }
}
