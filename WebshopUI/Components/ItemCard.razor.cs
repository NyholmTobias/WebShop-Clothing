using AutoMapper;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TabBlazor.Services;
using WebshopShared.RequestModels;
using WebshopShared.ResponseModels;
using WebshopUI.Services.Carts;
using WebshopUI.Services.Items;
using WebshopUI.Services.Users;

namespace WebshopUI.Components
{
    public partial class ItemCard : ComponentBase
    {
        [Parameter]
        public ItemResponse ItemResponse { get; set; }
        [Parameter]
        public bool Admin { get; set; }

        public int Quantity { get; set; }

        public string Output { get; set; } = "";
    
        [Inject]
        public ICartDataService CartDataService { get; set; }

        [Inject]
        public IItemDataService ItemDataService { get; set; }
        [Inject]
        public ToastService ToastService { get; set; }
        [Inject]
        public IMapper _mapper { get; set; }

        [Inject]
        public IUserDataService UserDataService { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        private SomethingWentWrong SomethingWentWrong { get; set; }

        public async Task AddToCart(ItemResponse itemResponse, int quantity)
        {
            if (itemResponse.StockQuantity >= quantity)
            {
                var identity = await AuthenticationStateProvider.GetAuthenticationStateAsync();
                var user = await UserDataService.GetUserByEmail(identity.User.Identity.Name);


                var lineitemResponse = new LineItemResponse
                {
                    Quantity = quantity,
                    Item = itemResponse,
                    ItemId = itemResponse.ItemId
                };

                SomethingWentWrong.CheckIfQuantityIsZero(lineitemResponse);
                if (lineitemResponse.Quantity > 0)
                {
                    await CartDataService.AddToCart(lineitemResponse, user.UserID);
                }
            }
            else
            {
                 await ToastService.AddToastAsync(new TabBlazor.ToastModel
                {
                    Title = "Woooopsie",
                    Message = $"Only {itemResponse.StockQuantity} left of {itemResponse.Name}."
                });
            }
           
        }

        public async Task AddToStock(ItemResponse itemResponse, int quantity)
        {
            itemResponse.StockQuantity = quantity;

            ItemResponse = await ItemDataService.UpdateItem(_mapper.Map<ItemRequest>(itemResponse));

            SomethingWentWrong.CheckIfItemResponseIsOk(ItemResponse);
            StateHasChanged();
        }
    }
}
