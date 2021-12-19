using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopServices.Features.Interfaces;
using WebshopServices.Validation.Interfaces;
using WebshopShared.IRepository;
using WebshopShared.Models;
using WebshopShared.RequestModels;
using WebshopShared.ResponseModels;

namespace WebshopServices.Features.LineItemServices
{
    public class LineItemService : ILineItemService
    {
        private readonly IMapper _mapper;
        private readonly ILineItemRepository _lineItemRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IValidatorFactory _validatorFactory;

        public LineItemService(IMapper mapper, ILineItemRepository lineItemRepository, IItemRepository itemRepository, IValidatorFactory validatorFactory)
        {
            _mapper = mapper;
            _lineItemRepository = lineItemRepository;
            _itemRepository = itemRepository;
            _validatorFactory = validatorFactory;
        }
        public async Task<LineItemResponse> CreateLineItem(LineItemRequest lineItemRequest)
        {
            var lineItemResponse = new LineItemResponse();

            lineItemResponse.AddValidationResult(await _validatorFactory.Get("LineItemRequestValidator").Validate(lineItemRequest));
            if (lineItemResponse.Success)
            {
                var lineitem = new LineItem()
                {
                    ItemId = lineItemRequest.ItemId,
                    OrderId = lineItemRequest.OrderId,
                    Quantity = lineItemRequest.Quantity,
                };

                await _lineItemRepository.AddAsync(lineitem);
                return _mapper.Map<LineItemResponse>(lineitem);
            }
            else
            {
                lineItemResponse = _mapper.Map<LineItemResponse>(lineItemRequest);
                return lineItemResponse;
            }
        }
        public double CalculateTotalOrderPrice(List<LineItem> lineItems)
        {
            double total = 0;

            lineItems.ForEach(lineItem =>
            {
                var linePrice = lineItem.Item.Price * lineItem.Quantity;
                total += linePrice;
            });
            return total;
        }
        public LineItem DecreseStockQuantity(LineItem lineItem)
        {
            lineItem.Item.StockQuantity -= lineItem.Quantity;
            return lineItem;
        }
    }
}
