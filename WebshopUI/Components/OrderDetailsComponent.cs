using AutoMapper;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using WebshopShared.RequestModels;
using WebshopShared.ResponseModels;
using WebshopUI.Services.Orders;

namespace WebshopUI.Components
{
    public partial class OrderDetailsComponent : ComponentBase
    {
        [Parameter]
        public Guid OrderId { get; set; }
        [Parameter]
        public bool Admin { get; set; }
        [Inject]
        public IOrderDataService OrderDataService { get; set; }
        [Inject]
        public IMapper _mapper { get; set; }

        public OrderResponse OrderResponse { get; set; }

        private SomethingWentWrong SomethingWentWrong { get; set; }

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
