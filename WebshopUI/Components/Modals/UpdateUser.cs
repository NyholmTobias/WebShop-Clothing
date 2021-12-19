using AutoMapper;
using DreamTeam.IDP.Shared.RequestModels;
using DreamTeam.IDP.Shared.ResponseModels;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebshopUI.Services.Users;

namespace WebshopUI.Components.Modals
{
    public partial class UpdateUser : ComponentBase
    {

        public UserRequest User { get; set; } = new UserRequest { };

        public UserResponse UserResponse { get; set; }

        [Inject]
        public IUserDataService UserDataService { get; set; }
        [Inject]
        public IMapper Mapper { get; set; }

        public bool ShowDialog { get; set; }


        public List<IdentityUserClaim<string>> AllClaims {get; set;}


        public List<IdentityUserClaim<string>> UserClaims { get; set; }

        public string UserId { get; set; }

        public string Username { get; set; }

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

        public async void Show(string userId, string username)
        {
            UserResponse = await UserDataService.GetUserById(userId);

            AllClaims = await UserDataService.GetAllClaims();
            UserClaims = await UserDataService.GetUsersClaims(userId);
            Username = username;
            UserId = userId;
            ShowDialog = true;
            StateHasChanged();
        }

        public void Close()
        {
            ShowDialog = false;
            StateHasChanged();
        }

        public async Task AddAdminClaims()
        {
            var claims = new List<IdentityUserClaim<string>>();
            claims.Add(new IdentityUserClaim<string> { ClaimType = "admin", ClaimValue = "admin" });
            claims.Add(new IdentityUserClaim<string> { ClaimType = "customer", ClaimValue = "customer" });

            await UserDataService.AddListOfClaimsToUser(Mapper.Map<UserRequest>(UserResponse), claims);
            StateHasChanged();
        }

        public async Task DeleteAdminClaims()
        {
            var claims = new List<IdentityUserClaim<string>>();
            claims.Add(new IdentityUserClaim<string> { ClaimType = "admin", ClaimValue = "admin" });

            var userRequest = Mapper.Map<UserRequest>(UserResponse);
            userRequest.Claims = claims;

            await UserDataService.DeleteListOfClaimsFromUser(userRequest);
            StateHasChanged();
        }
        public async Task AddCustomerClaims()
        {
            var claims = new List<IdentityUserClaim<string>>();
            claims.Add(new IdentityUserClaim<string> { ClaimType = "customer", ClaimValue = "customer" });

            await UserDataService.AddListOfClaimsToUser(Mapper.Map<UserRequest>(UserResponse), claims);
            StateHasChanged();
        }

        public async Task DeleteCustomerClaims()
        {
            var claims = new List<IdentityUserClaim<string>>();
            claims.Add(new IdentityUserClaim<string> { ClaimType = "customer", ClaimValue = "customer" });

            var userRequest = Mapper.Map<UserRequest>(UserResponse);
            userRequest.Claims = claims;

            await UserDataService.DeleteListOfClaimsFromUser(userRequest);
            StateHasChanged();
        }

        protected async Task HandleValidSubmit()
        {
            User = Mapper.Map<UserRequest>(UserResponse);
            UserResponse = await UserDataService.UpdateUser(User);
            ShowDialog = false;

            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }

        //private async void AddClaimToList(IdentityUserClaim<string> claim, bool checkedOrNot)
        //{
        //    var selectedClaims = new List<IdentityUserClaim<string>>();
        //    User = Mapper.Map<UserRequest>(UserResponse);
        //    if (checkedOrNot)
        //    {
        //        var claimToBeRemoved = UserClaims.Find(x => x.ClaimType == claim.ClaimType);
        //        UserClaims.Remove(claimToBeRemoved);
        //        selectedClaims.Add(claimToBeRemoved);
        //        User.Claims = selectedClaims;
        //        await UserDataService.DeleteListOfClaimsFromUser(User);

        //    }
        //    if (!checkedOrNot)
        //    {
        //        selectedClaims.Add(claim);
        //        await UserDataService.AddListOfClaimsToUser(User, selectedClaims);
        //    }

        //    UserClaims = await UserDataService.GetUsersClaims(UserId);
        //    StateHasChanged();
        //}

    }
}

