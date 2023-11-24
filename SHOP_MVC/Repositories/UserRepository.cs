using Microsoft.EntityFrameworkCore;
using SHOP_MVC.Data;
using SHOP_MVC.Interfaces;
using SHOP_MVC.Models;

namespace SHOP_MVC.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(User user)
        {
            await _context.AddAsync(user);
            return await SaveAsync();
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<User> GetUserAsync(string email)
        {
            return await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(x => x.Email == email);
        }
        public async Task<ICollection<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
