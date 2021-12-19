

using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TabBlazor;
using TabBlazor.Services;
using WebshopShared.Models;
using WebshopShared.ResponseModels;

namespace WebshopUI.Components
{
    public partial class SomethingWentWrong : ComponentBase
    {
        public MarkupString Text;
        [Inject]
        public ToastService ToastService { get; set; } = new ToastService { };


        private async Task ShowSomethingWentWrongToast(string message)
        {
            await ToastService.AddToastAsync(new ToastModel { Title = "Error!", Message = message });
        }
        private async Task ShowSuccessToast()
        {
            await ToastService.AddToastAsync(new ToastModel { Title = "Success", Message = "Success!" });
        }
        private async Task ShowSuccessToast(string message)
        {
            await ToastService.AddToastAsync(new ToastModel { Title = "Success", Message = message });
        }

        public async Task MustacheMan()
        {
            var renderfragment = "<img style='max-width:100px; max-height:200px;' src='images/MustacheMan.jpg'/>";
            var test = new ToastModel("title", "subTitle", renderfragment);
            await ToastService.AddToastAsync(test);
        }

        public async void CheckIfSomethingWentWrongWithOrder(OrderResponse orderResponse)
        {
            var listOfValidationErrors = new List<string>();

            if (orderResponse.Success == false && orderResponse.ValidationErrors != null)
            {
                listOfValidationErrors.AddRange(orderResponse.ValidationErrors);
            }
            foreach (var lineItem in orderResponse.LineItems)
            {
                
                if (lineItem.Success == false && lineItem.ValidationErrors != null)
                {
                    listOfValidationErrors.AddRange(lineItem.ValidationErrors);
                }
            }
            if (listOfValidationErrors.Count > 0)
            {
                var distinctListOfErros = listOfValidationErrors.Distinct<string>();
                foreach (var validationError in distinctListOfErros)
                {
                    await ShowSomethingWentWrongToast(validationError);
                }
            }
            else
            {
                await ShowSuccessToast();
            }

        }

        public async void CheckIfSomethingWentWrongWithListOfOrders(List<OrderResponse> orderResponses)
        {
            var listOfValidationErrors = new List<string>();
            foreach (var orderResponse in orderResponses)
            {
                if (orderResponse.Success == false && orderResponse.ValidationErrors != null)
                {
                    listOfValidationErrors.AddRange(orderResponse.ValidationErrors);
                }
            }
            if (listOfValidationErrors.Count > 0)
            {
                foreach (var validationError in listOfValidationErrors)
                {
                    await ShowSomethingWentWrongToast(validationError);
                }
            }
            else
            {
                await ShowSuccessToast();
            }

        }

        public async void CheckIfQuantityIsZero(LineItemResponse lineITem)
        {
            if (lineITem.Quantity == 0)
            {
                await ShowSomethingWentWrongToast("Cant add 0 to cart!");
            }
            else
            {
                await ShowSuccessToast("Added to cart!");
            }

        }

        public async void CheckIfItemResponseIsOk(ItemResponse itemResponse)
        {
            if (itemResponse.Success == false)
            {
                await ShowSomethingWentWrongToast("Something went wrong");
            }
            else
            {
                await ShowSuccessToast($"Stock changed to: {itemResponse.StockQuantity}");
            }

        }
        
        



    }
}
