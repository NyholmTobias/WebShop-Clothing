using FluentValidation.Results;
using System.Collections.Generic;

namespace WebshopShared.ResponseModels
{
    public class BaseResponse
    {
        public BaseResponse()
        {
            Success = true;
        }

        public BaseResponse(string message = null)
        {
            Success = true;
            Message = message;
        }

        public BaseResponse(string message, bool successs)
        {
            Success = successs;
            Message = message;

        }

        public BaseResponse AddValidationResult(ValidationResult validationResult)
        {
            if (validationResult.Errors.Count > 0)
            {
                this.Success = false;
                this.ValidationErrors = new List<string>();

                foreach (var error in validationResult.Errors)
                {
                    this.ValidationErrors.Add(error.ErrorMessage);
                }
            }

            return this;
        }

        public bool Success { get; set; }
        public string Message { get; set; }
        public List<string> ValidationErrors { get; set; }
    }

}
