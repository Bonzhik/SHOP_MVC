using Microsoft.EntityFrameworkCore;
using SHOP_MVC.Data;
using SHOP_MVC.Interfaces;
using SHOP_MVC.Models;

namespace SHOP_MVC.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ApplicationDbContext _context;
        public ProductRepository(ApplicationDbContext context) {
            _context = context;
        }

        public async Task<bool> AddAsync(Product product)
        {
            await _context.AddAsync(product);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(Product product)
        {
            _context.Remove(product); 
            return await SaveAsync(); 
        }

        public async Task<ICollection<Product>> GetProductsAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<bool> IsExistsAsync(Product product)
        {
            if (await _context.Products.AnyAsync(c => c.Title == product.Title && c.Id != product.Id))
            {
                return true;
            }
            return false;
        }

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(Product product)
        {
            _context.Update(product);
            return await SaveAsync();
        }
    }
}
