using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopServices.Features.Interfaces;
using WebshopServices.Validation.Interfaces;
using WebshopShared.IRepository;
using WebshopShared.Models;
using WebshopShared.RequestModels;
using WebshopShared.ResponseModels;

namespace WebshopServices.Features.ItemServices
{
    public class ItemService : IItemService
    {
        private readonly IMapper _mapper;
        private readonly IItemRepository _itemRepository;
        private readonly IValidatorFactory _validatorFactory;
        private readonly ILineItemRepository _lineItemRepository;
        private readonly IOrderRepository _orderRepository;

        public ItemService(IMapper mapper, IItemRepository itemRepository, IValidatorFactory validatorFactory, ILineItemRepository lineItemRepository, IOrderRepository orderRepository)
        {
            _mapper = mapper;
            _itemRepository = itemRepository;
            _validatorFactory = validatorFactory;
            _lineItemRepository = lineItemRepository;
            _orderRepository = orderRepository;
        }

        public async Task<ItemResponse> CreateItem(ItemRequest itemRequest)
        {
            var itemResponse = new ItemResponse();
            
            itemResponse.AddValidationResult(await _validatorFactory.Get("ItemRequestValidator").Validate(itemRequest));
            if (itemResponse.Success)
            {
                var item = new Item()
                {
                    Name = itemRequest.Name,
                    Price = itemRequest.Price,
                    Description = itemRequest.Description,
                    StockQuantity = itemRequest.StockQuantity,
                    Published = itemRequest.Published,
                    PictureSourcePath = itemRequest.PictureSourcePath
                };

                await _itemRepository.AddAsync(item);
                return _mapper.Map<ItemResponse>(item);
            }
            else
            {
                itemResponse = _mapper.Map<ItemResponse>(itemRequest);
                return itemResponse;
            }
        }

        public async Task<Guid> DeleteItem(Guid itemId)
        {
            var ItemToBeDeleted = await _itemRepository.GetByIdAsync(itemId);
            await _itemRepository.DeleteAsync(ItemToBeDeleted);

            return ItemToBeDeleted.ItemId;
        }

        public async Task<List<ItemResponse>> GetAllItems()
        {
            var items = await _itemRepository.ListAllAsync();
            var itemResponse = _mapper.Map<List<ItemResponse>>(items);

            return itemResponse;
        }

        public async Task<ItemResponse> GetItemById(Guid itemId)
        {
            var item = await _itemRepository.GetByIdAsync(itemId);

            return _mapper.Map<ItemResponse>(item);
        }

        public async Task<ItemResponse> UpdateItem(ItemRequest itemRequest)
        {
            var itemResponse = new ItemResponse();

            itemResponse.AddValidationResult(await _validatorFactory.Get("ItemRequestValidator").Validate(itemRequest));
            if (itemResponse.Success)
            {
                var editedItem = new Item()
                {
                    Name = itemRequest.Name,
                    Price = itemRequest.Price,
                    StockQuantity = itemRequest.StockQuantity,
                    Description = itemRequest.Description,
                    Published = itemRequest.Published,
                };
                if (itemRequest.PictureSourcePath != null)
                {
                    editedItem.PictureSourcePath = itemRequest.PictureSourcePath;
                }

                await _itemRepository.UpdateAsync(editedItem);
                return _mapper.Map<ItemResponse>(editedItem);
            }
            else
            {
                itemResponse = _mapper.Map<ItemResponse>(itemRequest);
                return itemResponse;
            }
        }

    }
}
