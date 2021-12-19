using FluentValidation;
using FluentValidation.Results;
using System.Threading.Tasks;
using WebshopShared.Interfaces;
using WebshopShared.IRepository;
using WebshopShared.RequestModels;
using IValidator = WebshopServices.Validation.Interfaces.IValidator;

namespace WebshopServices.Validation
{
    public class CategoryRequestValidator : AbstractValidator<CategoryRequest>, IValidator
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryRequestValidator(ICategoryRepository categoryRepository)
        {
            //This is not used yet but is here for future usage.
            _categoryRepository = categoryRepository;

            RuleFor(c => c.Name)
                .NotEmpty()
                .WithMessage("A category must have a name.");
        }

        public async Task<ValidationResult> Validate(IValidatable categoryRequest)
        {
            return await ValidateAsync((CategoryRequest)categoryRequest);
        }
    }
}
