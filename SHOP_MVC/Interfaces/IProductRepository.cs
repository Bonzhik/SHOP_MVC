using SHOP_MVC.Models;

namespace SHOP_MVC.Interfaces
{
    public interface IProductRepository
    {
        Task<ICollection<Product>> GetProductsAsync();
        Task<bool> AddAsync(Product product);
        Task<bool> UpdateAsync(Product product);
        Task<bool> DeleteAsync(Product product);
        Task<bool> IsExistsAsync(Product product);
        Task<bool> SaveAsync();
    }
}
