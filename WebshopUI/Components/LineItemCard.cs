using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using WebshopShared.ResponseModels;
using WebshopUI.Services.Carts;

namespace WebshopUI.Components
{
    public partial class LineItemCard
    {
        [Parameter]
        public LineItemResponse LineItemResponse { get; set; }
        [Parameter]
        public Guid UserId { get; set; }

        [Inject]
        public ICartDataService CartDataService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        public async Task DeleteLineItem(LineItemResponse lineItem)
        {
            await CartDataService.RemoveFromCart(lineItem, UserId);
            NavigationManager.NavigateTo("cart", true);
            StateHasChanged();
        }
    }
}
