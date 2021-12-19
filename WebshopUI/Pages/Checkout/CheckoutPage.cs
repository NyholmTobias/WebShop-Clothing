using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Timers;
using TabBlazor.Services;
using WebshopShared.ResponseModels;
using WebshopUI.Components;
using WebshopUI.Services.Carts;
using WebshopUI.Services.Users;

namespace WebshopUI.Pages.Checkout
{
    public partial class CheckoutPage : ComponentBase
    {
        [Inject]
        public ICartDataService CartDataService { get; set; }
        [Inject]
        public IUserDataService UserDataService { get; set; }
        [Inject]
        public ToastService ToastService { get; set; }
        [Inject]
        public NavigationManager NavigationManager { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
        public List<LineItemResponse> LineItems { get; set; }
        private LineItemCard LineItemCard { get; set; }
        private Guid UserId { get; set; }
        private SomethingWentWrong SomethingWentWrong { get; set; }
        


        protected override async Task OnInitializedAsync()
        {
            var identity = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            
            var user = await UserDataService.GetUserByEmail(identity.User.Identity.Name);
            
            UserId = user.UserID;
            LineItems = await CartDataService.GetShoppingCart(UserId);

        }

        public async Task CreateOrder()
        {
            var orderResponse = await CartDataService.CreateOrderFromCart(UserId);
            SomethingWentWrong.CheckIfSomethingWentWrongWithOrder(orderResponse);
            StateHasChanged();
            if (orderResponse.Success)
            {
                await ToastService.AddToastAsync(new TabBlazor.ToastModel { Title = "Redirecting..", Message = $"You are being redirected..." });
                var timer = new Timer { Interval = 3000, AutoReset = false };
                timer.Elapsed += new ElapsedEventHandler(AfterCreatedOrder);
                timer.Start();
            }
            
        }

        private async void AfterCreatedOrder(object sender, ElapsedEventArgs e)
        {
            
            await CartDataService.ClearCart(UserId);
            NavigationManager.NavigateTo("/");
        }

        public async Task ClearCart()
        {
            await CartDataService.ClearCart(UserId);
            NavigationManager.NavigateTo("cart", true);
            await ToastService.AddToastAsync(new TabBlazor.ToastModel { Title = "Success!", Message = "Cart cleared!" });
        }

        
    }
}
