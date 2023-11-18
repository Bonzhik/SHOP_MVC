using Microsoft.AspNetCore.Mvc.Rendering;
using SHOP_MVC.Models.Dto;

namespace SHOP_MVC.Models.ViewModels
{
    public class CEProductViewModel
    {
        public ProductDto ProductDto { get; set; }
        public IEnumerable<SelectListItem> AllCategories { get; set; }
        public List<int> SelectedCategories { get; set; }

    }
}
