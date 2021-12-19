using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopShared.ResponseModels;

namespace WebshopUI.Services.Carts
{
    public interface ICartDataService
    {
        Task AddToCart(LineItemResponse lineItem, Guid guid);
        Task RemoveFromCart(LineItemResponse lineItem, Guid guid);
        Task ClearCart(Guid guid);
        Task<OrderResponse> CreateOrderFromCart(Guid guid);
        Task<List<LineItemResponse>> GetShoppingCart(Guid guid);
    }
}
