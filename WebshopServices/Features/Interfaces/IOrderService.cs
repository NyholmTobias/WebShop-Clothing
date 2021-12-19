using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopShared.Models;
using WebshopShared.RequestModels;
using WebshopShared.ResponseModels;

namespace WebshopServices.Features.Interfaces
{
    public interface IOrderService
    {
        Task<OrderResponse> CreateOrder(OrderRequest orderRequest);
        Task<List<OrderResponse>> GetAllOrders();
        Task<OrderResponse> GetOrderById(Guid orderId);
        Task<OrderResponse> UpdateOrder(OrderRequest orderRequest);
        Task<OrderResponse> DeleteOrder(Guid orderId);

        Task<List<OrderResponse>> GetOrdersByUser(Guid userId);
    }
}
