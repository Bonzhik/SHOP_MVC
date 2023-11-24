using SHOP_MVC.Models;

namespace SHOP_MVC.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role> GetRoleAsync(string title);
    }
}
