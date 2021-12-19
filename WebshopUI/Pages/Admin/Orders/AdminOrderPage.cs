using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopShared.ResponseModels;
using WebshopUI.Components;
using WebshopUI.Services.Orders;

namespace WebshopUI.Pages.Admin.Orders
{
    public partial class AdminOrderPage : ComponentBase
    {
        
        [Inject]
        public IOrderDataService OrderDataService { get; set; }
        public List<OrderResponse> OrderResponses { get; set; }

        private OrderTable OrderTable { get; set; }


        protected override async Task OnInitializedAsync()
        {
            OrderResponses = await OrderDataService.GetAllOrders();
        }
       
    }
}
