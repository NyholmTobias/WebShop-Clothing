using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopShared.RequestModels;
using WebshopShared.ResponseModels;

namespace WebshopServices.Features.Interfaces
{
    public interface IItemService
    {
        Task<ItemResponse> CreateItem(ItemRequest itemRequest);
        Task<Guid> DeleteItem(Guid itemId);
        Task<ItemResponse> UpdateItem(ItemRequest itemRequest);
        Task<List<ItemResponse>> GetAllItems();
        Task<ItemResponse> GetItemById(Guid itemId);
    }
}
