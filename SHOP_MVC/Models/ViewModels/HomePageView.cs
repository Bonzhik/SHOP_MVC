namespace SHOP_MVC.Models.ViewModels
{
    public class HomePageView
    {
        public ICollection<Product> Products { get; set; }
        public ICollection<Category> Categories { get; set; }
    }
}
