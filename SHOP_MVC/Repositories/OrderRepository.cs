using Microsoft.EntityFrameworkCore;
using SHOP_MVC.Data;
using SHOP_MVC.Interfaces;
using SHOP_MVC.Models;

namespace SHOP_MVC.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;

        public OrderRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> AddOrderAsync(List<OrderProduct> orderProducts, Order order)
        {
            await _context.AddAsync(order);
            foreach (var orderProduct in orderProducts)
            {
                var currentproduct = await _context.Products.FirstOrDefaultAsync(p => p.Equals(orderProduct.Product));
                currentproduct.Quantity -= orderProduct.Quantity;
                await _context.AddAsync(orderProduct);
            }
            return await SaveAsync();
        }
        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
