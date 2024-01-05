namespace SHOP_MVC.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public string Status { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
        public User User { get; set; }

    }
}
