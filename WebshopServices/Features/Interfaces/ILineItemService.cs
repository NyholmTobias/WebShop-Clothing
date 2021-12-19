using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopShared.Models;
using WebshopShared.RequestModels;
using WebshopShared.ResponseModels;

namespace WebshopServices.Features.Interfaces
{
    public interface ILineItemService
    {
        Task<LineItemResponse> CreateLineItem(LineItemRequest lineItemRequest);
        public double CalculateTotalOrderPrice(List<LineItem> lineItems);
        public LineItem DecreseStockQuantity(LineItem lineItem);
    }
}
