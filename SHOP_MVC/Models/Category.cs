using System.ComponentModel.DataAnnotations;

namespace SHOP_MVC.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; } = new();
    }
}
