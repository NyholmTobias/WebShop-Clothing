using System;
using WebshopServices.Validation.Interfaces;
using WebshopShared.IRepository;

namespace WebshopServices.Validation
{
    public class ValidatorFactory : IValidatorFactory
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IItemRepository _itemRepository;
        private readonly ILineItemRepository _lineItemRepository;
        private readonly ICategoryRepository _categoryRepository;

        public ValidatorFactory(IOrderRepository orderRepository, IItemRepository itemRepository, ILineItemRepository lineItemRepository, ICategoryRepository categoryRepository)
        {
            _orderRepository = orderRepository;
            _itemRepository = itemRepository;
            _lineItemRepository = lineItemRepository;
            _categoryRepository = categoryRepository;
        }

        public IValidator Get(string name ) => name switch
        {
            "OrderRequestValidator" => new OrderRequestValidator(_orderRepository),
            "ItemRequestValidator" => new ItemRequestValidator(_itemRepository),
            "LineItemRequestValidator" => new LineItemRequestValidator(_lineItemRepository, _orderRepository),
            "CategoryRequestValidator" => new CategoryRequestValidator(_categoryRepository),
            _ => throw new ApplicationException(string.Format("Validator {name} cant be found!")),
        };
    }
}
