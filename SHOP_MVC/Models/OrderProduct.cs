namespace SHOP_MVC.Models
{
    public class OrderProduct
    {
        public int OrderId { get; set; }
        public Order Order { get; } = new();

        public int ProductId { get; set; }
        public Product Product { get; } = new();

        public int Quantity { get; set; }
    }
}
