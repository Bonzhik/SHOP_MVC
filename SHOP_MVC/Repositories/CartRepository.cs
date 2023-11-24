using SHOP_MVC.Data;
using SHOP_MVC.Interfaces;
using SHOP_MVC.Models;

namespace SHOP_MVC.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<bool> AddAsync(CartItem item)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CartItem> GetCartItemAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<CartItem>> GetCartItemsAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(CartItem item)
        {
            throw new NotImplementedException();
        }
    }
}
