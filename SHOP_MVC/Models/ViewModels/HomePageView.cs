using SHOP_MVC.Models.Dto;

namespace SHOP_MVC.Models.ViewModels
{
    public class HomePageView
    {
        public ICollection<ProductDto> Products { get; set; }
        public ICollection<CategoryDto> Categories { get; set; }
    }
}
