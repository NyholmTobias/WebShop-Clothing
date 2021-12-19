using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopShared.RequestModels;
using WebshopShared.ResponseModels;

namespace WebshopUI.Services.Orders
{
    public interface IOrderDataService
    {
        Task<OrderResponse> CreateOrder(OrderRequest orderRequest);
        Task<List<OrderResponse>> GetAllOrders();
        Task<OrderResponse> GetOrderById(Guid orderId);
        Task<OrderResponse> UpdateOrder(OrderRequest orderRequest);

        Task<List<OrderResponse>> GetAllOrdersByUser(Guid userId);
        Task<OrderResponse> DeleteOrder(Guid orderId);

    }
}
