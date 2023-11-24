using SHOP_MVC.Models;

namespace SHOP_MVC.Interfaces
{
    public interface ICartRepository
    {
        Task<CartItem> GetCartItemAsync(int id);
        Task<ICollection<CartItem>> GetCartItemsAsync(int userId);
        Task<bool> AddAsync(CartItem item);
        Task<bool> UpdateAsync(CartItem item);
        Task<bool> DeleteAsync(int id);
    }
}
