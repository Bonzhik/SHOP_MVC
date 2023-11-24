namespace SHOP_MVC.Models
{
    public class CartItem
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }
        public int Quantity { get; set; }

    }
}
