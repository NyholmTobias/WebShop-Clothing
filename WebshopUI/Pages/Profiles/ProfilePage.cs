using DreamTeam.IDP.Shared.ResponseModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopShared.ResponseModels;
using WebshopUI.Components;
using WebshopUI.Services.Orders;
using WebshopUI.Services.Users;

namespace WebshopUI.Pages.Profiles
{
    public partial class ProfilePage : ComponentBase
    {
        public UserResponse UserResponse { get; set; }
        public List<OrderResponse> OrderResponses { get; set; }
        [Inject]
        public IOrderDataService OrderDataService { get; set; }
        [Inject]
        public IUserDataService UserDataService { get; set; }
        [Inject]
        public AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        private OrderTable OrderTable { get; set; }
        protected override async Task OnInitializedAsync()
        {
            var identity = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            UserResponse = await UserDataService.GetUserByEmail(identity.User.Identity.Name);
            OrderResponses = await OrderDataService.GetAllOrdersByUser(UserResponse.UserID);
            StateHasChanged();

            
        }
    }
}
