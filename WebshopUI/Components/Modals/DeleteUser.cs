
using DreamTeam.IDP.Shared.ResponseModels;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WebshopUI.Services.Users;

namespace WebshopUI.Components.Modals
{
    public partial class DeleteUser : ComponentBase
    {
        public UserResponse User { get; set; } = new UserResponse { };
        [Inject]
        public IUserDataService UserDataService { get; set;}
        [Parameter]
        public bool ShowDialog {get;set;}

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

        public void Show()
        {

            ShowDialog = true;
            StateHasChanged();
        }
        public void Show(UserResponse user)
        {
            User = user;
            ShowDialog = true;
            StateHasChanged();
        }
        public void Close()
        {
            ShowDialog = false;
            StateHasChanged();
        }

        protected async Task DeleteUserAsync(string email)
        {
            await UserDataService.DeleteUser(email);
            ShowDialog = false;

            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }


    }

}

