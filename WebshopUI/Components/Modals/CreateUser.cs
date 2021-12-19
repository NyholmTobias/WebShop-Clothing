
using DreamTeam.IDP.Shared.RequestModels;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using WebshopUI.Services.Users;

namespace WebshopUI.Components.Modals
{
    public partial class CreateUser : ComponentBase
    {
        public UserRequest User { get; set; } =
           new UserRequest { };

        [Inject]
        public IUserDataService UserDataService { get; set; }

        public bool ShowDialog { get; set; }

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

        public void Show()
        {
            ResetDialog();
            ShowDialog = true;
            StateHasChanged();
        }

        public void Close()
        {
            ShowDialog = false;
            StateHasChanged();
        }

        private void ResetDialog()
        {
            User = new UserRequest { };
        }

        protected async Task HandleValidSubmit()
        {
            await UserDataService.CreateUser(User);
            ShowDialog = false;

            await CloseEventCallback.InvokeAsync(true);
            StateHasChanged();
        }

    }
}
