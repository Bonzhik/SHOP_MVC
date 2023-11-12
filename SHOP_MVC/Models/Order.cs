namespace SHOP_MVC.Models
{
    public class Order
    {
        public int Id { get; set; }
        public List<Product> Products { get; } = new();
        public Status Status { get; } = new();
        public List<OrderProduct> OrderProducts { get; } = new();
        public User User { get; } = new ();

    }
}
