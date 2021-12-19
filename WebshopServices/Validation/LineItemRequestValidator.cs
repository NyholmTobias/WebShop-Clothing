using FluentValidation;
using FluentValidation.Results;
using System;
using System.Threading.Tasks;
using WebshopShared.Interfaces;
using WebshopShared.IRepository;
using WebshopShared.Models;
using WebshopShared.RequestModels;
using IValidator = WebshopServices.Validation.Interfaces.IValidator;

namespace WebshopServices.Validation
{
    public class LineItemRequestValidator : AbstractValidator<LineItemRequest>, IValidator
    {
        private readonly ILineItemRepository _lineItemRepository;
        private readonly IOrderRepository _orderRepository;

        public LineItemRequestValidator(ILineItemRepository lineItemRepository, IOrderRepository orderRepository)
        {
            // This is not used yet, but is here for future usage. 
            _lineItemRepository = lineItemRepository;
            _orderRepository = orderRepository;

            RuleFor(li => li.Quantity)
                .GreaterThan(0)
                .WithMessage("The quantity must be greater than 0.");

            RuleFor(li => li)
                .Must(IsLineItemQuantityLessThanStockQuantity)
                .WithMessage("The quantity can't be greater than the stock quantity.");

            
        }
        public async Task<ValidationResult> Validate(IValidatable lineItemRequest)
        {
            return await ValidateAsync((LineItemRequest)lineItemRequest);
        }

        private bool IsLineItemQuantityLessThanStockQuantity(LineItemRequest lineItemRequest)
        {
            return lineItemRequest.Quantity <= lineItemRequest.Item.StockQuantity ? true : false;
        }

        
    }
}
