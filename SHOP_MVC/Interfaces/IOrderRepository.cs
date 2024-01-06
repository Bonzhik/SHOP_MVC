using SHOP_MVC.Models;

namespace SHOP_MVC.Interfaces
{
    public interface IOrderRepository
    {
        Task<bool> AddOrderAsync(List<OrderProduct> orderProducts, Order order);
        Task<bool> SaveAsync();
    }
}
