using SHOP_MVC.Models;

namespace SHOP_MVC.Interfaces
{
    public interface ICategoryRepository
    {
        Task<ICollection<Category>> GetCategoriesAsync();
        Task<bool> AddAsync(Category category);
        Task<bool> UpdateAsync(Category category);
        Task<bool> DeleteAsync(Category category);
        Task<bool> IsExistsAsync(Category category);
        Task<bool> SaveAsync();
    }
}
