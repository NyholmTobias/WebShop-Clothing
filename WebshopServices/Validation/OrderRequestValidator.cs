using FluentValidation;
using FluentValidation.Results;
using System.Threading.Tasks;
using WebshopShared.Interfaces;
using WebshopShared.IRepository;
using WebshopShared.RequestModels;
using IValidator = WebshopServices.Validation.Interfaces.IValidator;

namespace WebshopServices.Validation
{
    public class OrderRequestValidator : AbstractValidator<OrderRequest>, IValidator
    {
        private readonly IOrderRepository _orderRepository;

        public OrderRequestValidator(IOrderRepository orderRepository)
        {
            //This is not used yet but is here for future usage.
            _orderRepository = orderRepository;

            RuleForEach(o => o.LineItems)
                .NotEmpty()
                .WithMessage("Orders can't contain empty items.");

        }

        public async Task<ValidationResult> Validate(IValidatable orderRequest)
        {
            return await ValidateAsync((OrderRequest)orderRequest);
        }
    }
}
