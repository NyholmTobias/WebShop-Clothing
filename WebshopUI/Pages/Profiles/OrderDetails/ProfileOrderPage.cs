using AutoMapper;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using WebshopShared.ResponseModels;
using WebshopUI.Components;
using WebshopUI.Services.Orders;

namespace WebshopUI.Pages.Profiles.OrderDetails
{
    public partial class ProfileOrderPage : ComponentBase
    {
        [Parameter]
        public Guid OrderId { get; set; }
        [Inject]
        public IOrderDataService OrderDataService { get; set; }
        
        public OrderResponse OrderResponse { get; set; }

        private OrderDetailsComponent OrderDetailsComponent { get; set; }


        protected override async Task OnInitializedAsync()
        {
            OrderResponse = await OrderDataService.GetOrderById(OrderId);
        }

    }
}
