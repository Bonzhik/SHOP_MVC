namespace SHOP_MVC.Models
{
    public class Order
    {
        public int Id { get; set; }
        public Status Status { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
        public User User { get; set; }

    }
}
