using FluentValidation;
using FluentValidation.Results;
using System.Threading.Tasks;
using WebshopShared.Interfaces;
using WebshopShared.IRepository;
using WebshopShared.RequestModels;
using IValidator = WebshopServices.Validation.Interfaces.IValidator;

namespace WebshopServices.Validation
{
    public class ItemRequestValidator : AbstractValidator<ItemRequest>, IValidator
    {
        private readonly IItemRepository _itemRepository;

        public ItemRequestValidator(IItemRepository itemRepository)
        {
            // This is not used yet, but is here for future usage. 
            _itemRepository = itemRepository;

            RuleFor(Item => Item)
                .NotEmpty()
                .WithMessage("Items can't be empty");

            RuleFor(Item => Item.Name)
                .NotEmpty()
                .WithMessage("Items must have a name.");

            RuleFor(Item => Item.Price)
                .NotEmpty()
                .WithMessage("Items must have a price.");

            RuleFor(Item => Item.Description)
                .NotEmpty()
                .WithMessage("Items must have a description.");

            RuleFor(Item => Item.PictureSourcePath)
                .NotEmpty()
                .WithMessage("Items must have a picture source path.");
        }
        public async Task<ValidationResult> Validate(IValidatable itemRequest)
        {
            return await ValidateAsync((ItemRequest)itemRequest);
        }
    }
}
