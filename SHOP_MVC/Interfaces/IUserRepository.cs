using SHOP_MVC.Models;

namespace SHOP_MVC.Interfaces
{
    public interface IUserRepository
    {
        Task<ICollection<User>> GetUsersAsync();
        Task<User> GetUserAsync(int id);
        Task<User> GetUserAsync(string email);
        Task<bool> AddAsync(User user);
        Task<bool> SaveAsync();

    }
}
