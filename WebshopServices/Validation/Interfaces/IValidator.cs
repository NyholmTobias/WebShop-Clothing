using FluentValidation.Results;
using System.Threading.Tasks;
using WebshopShared.Interfaces;

namespace WebshopServices.Validation.Interfaces
{
    public interface IValidator
    {
        Task<ValidationResult> Validate(IValidatable iValidatable);
    }
}
