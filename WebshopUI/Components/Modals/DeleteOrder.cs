using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using WebshopUI.Services.Orders;

namespace WebshopUI.Components.Modals
{
    public partial class DeleteOrder : ComponentBase
    {
        [Parameter]
        public Guid OrderId { get; set; }
        [Inject]
        public IOrderDataService OrderDataService { get; set; }
        [Parameter]
        public bool ShowDialog { get; set; }

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

        private SomethingWentWrong SomethingWentWrong { get; set; }

        public void Show()
        {

            ShowDialog = true;
            StateHasChanged();
        }
        
        public void Close()
        {
            ShowDialog = false;
            StateHasChanged();
        }

        protected async Task DeleteOrderAsync()
        {
            var orderResponse = await OrderDataService.DeleteOrder(OrderId);
            SomethingWentWrong.CheckIfSomethingWentWrongWithOrder(orderResponse);
            ShowDialog = false;
            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }
    }
}
