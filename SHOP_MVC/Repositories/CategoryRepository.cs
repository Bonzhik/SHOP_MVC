using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SHOP_MVC.Data;
using SHOP_MVC.Interfaces;
using SHOP_MVC.Models;

namespace SHOP_MVC.Repositories
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<ICollection<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }
        public async Task<Category> GetCategoryAsync(int id)
        {
            return await _context.Categories.FirstOrDefaultAsync(c => c.Id == id);
        }
        public async Task<bool> AddAsync(Category category)
        {
            await _context.AddAsync(category);
            return await SaveAsync();
        }

        public async Task<bool> UpdateAsync(Category category)
        {
            _context.Update(category);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(Category category)
        {
            _context.Remove(category);
            return await SaveAsync();
        }

        public async Task<bool> IsExistsAsync(Category category)
        {
            if (await _context.Categories.AnyAsync(c => c.Title == category.Title && c.Id != category.Id)) {
                return true;
            }
            return false;
        }
        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
