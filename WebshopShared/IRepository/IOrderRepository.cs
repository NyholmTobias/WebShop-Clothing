using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopShared.Models;

namespace WebshopShared.IRepository
{
    public interface IOrderRepository : IAsyncRepository<Order>
    {
        Task<Order> GetOrderById(Guid OrderId);
        Task<List<Order>> GetOrdersByUserId(Guid userId);
    }
}
