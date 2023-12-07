using Microsoft.EntityFrameworkCore;
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
            await _context.AddAsync(item);
            return await SaveAsync();

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.CartItems.FirstOrDefaultAsync(x => x.Id == id);
            _context.Remove(product);
            return await SaveAsync();
        }

        public async Task<CartItem> GetCartItemAsync(int id)
        {
            return await _context.CartItems.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ICollection<CartItem>> GetCartItemsAsync(string email)
        {
            return await _context.CartItems.Include(p => p.Product).Include(p => p.User).Where(p => p.User.Email == email).ToListAsync();

        }

        public async Task<bool> UpdateAsync(CartItem item)
        {
            _context.Update(item);
            return await SaveAsync();
        }
        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
