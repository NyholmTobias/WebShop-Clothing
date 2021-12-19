using AutoMapper;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using WebshopShared.RequestModels;
using WebshopShared.ResponseModels;
using WebshopUI.Components;
using WebshopUI.Services.Orders;

namespace WebshopUI.Pages.Admin.EditOrder
{
    public partial class EditOrderPage : ComponentBase
    {
        [Parameter]
        public Guid OrderId { get; set; }
        [Inject]
        public IOrderDataService OrderDataService { get; set; }
        [Inject]
        public IMapper _mapper { get; set; }

        public OrderResponse OrderResponse { get; set; }

        private SomethingWentWrong SomethingWentWrong { get; set; }

        private OrderDetailsComponent OrderDetailsComponent { get; set; }

        protected override async Task OnInitializedAsync()
        {
            OrderResponse = await OrderDataService.GetOrderById(OrderId);
        }

        public async Task RemoveLineItem(LineItemResponse lineItemResponse)
        {
            OrderResponse.LineItems.Remove(lineItemResponse);
            OrderResponse = await OrderDataService.UpdateOrder(_mapper.Map<OrderRequest>(OrderResponse));
            SomethingWentWrong.CheckIfSomethingWentWrongWithOrder(OrderResponse);
            StateHasChanged();
        }
    }
}
