using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopShared.RequestModels;
using WebshopShared.ResponseModels;

namespace WebshopUI.Services.Items
{
    public interface IItemDataService
    {
        Task<List<ItemResponse>> GetAllItems();
        Task<ItemResponse> UpdateItem(ItemRequest itemRequest);
    }
}
