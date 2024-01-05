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
        public async Task<bool> AddOrder(int[][] productsId, Order order)
        {
            await _context.AddAsync(order);
            for (int i = 0; i < productsId.Length; i++)
            {
                var currentproduct = await _context.Products.FirstOrDefaultAsync(p => p.Id == productsId[i][0]);
                currentproduct.Quantity -= productsId[i][1];

                OrderProduct orderProductEntity = new OrderProduct()
                {
                    Order = order,
                    Product = currentproduct,
                    Quantity = productsId[i][1]
                };
                await _context.AddAsync(orderProductEntity);
            }
            return await SaveAsync();
        }
        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
