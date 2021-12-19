using System;

namespace WebshopServices.Validation.Interfaces
{
    public interface IValidatorFactory
    {
        IValidator Get(string name);
    }
}
