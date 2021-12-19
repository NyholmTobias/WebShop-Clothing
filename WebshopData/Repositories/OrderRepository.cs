using Microsoft.EntityFrameworkCore;
using WebshopShared.IRepository;
using WebshopShared.Models;
using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;

namespace WebshopData.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        protected new readonly WebshopDbContext _WebshopDbContext;
        public OrderRepository(WebshopDbContext webshopDbContext) : base(webshopDbContext)
        {
            _WebshopDbContext = webshopDbContext;
        }

        public async Task<Order> GetOrderById(Guid OrderId)
        {
            var order = await _WebshopDbContext.Orders
                .Include(o => o.LineItems)
                .ThenInclude(li => li.Item).FirstOrDefaultAsync(o => o.OrderId == OrderId);

            return order;
        }

        public async Task<List<Order>> GetOrdersByUserId(Guid userId)
        {
            return await _WebshopDbContext.Orders
                .Where(order => order.UserId == userId).ToListAsync();
               
        }

    }
}
