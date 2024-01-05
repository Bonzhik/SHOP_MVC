using SHOP_MVC.Models;

namespace SHOP_MVC.Interfaces
{
    public interface IOrderRepository
    {
        Task<bool> AddOrder(int[][] productsId, Order order);
        Task<bool> SaveAsync();
    }
}
