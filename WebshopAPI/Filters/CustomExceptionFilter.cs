using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;

namespace WebshopAPI.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        //private readonly ILogger _logger;
        public CustomExceptionFilter()
        {
            
        }
        public void OnException(ExceptionContext context)
        {

            if (context.Exception != null)
            {
                var guid = Guid.NewGuid();
                //Kan logga om man vill men använder bara som debugger just nu
                //_logger.Error(context.Exception, guid.ToString());
            }
        }
    }
}
