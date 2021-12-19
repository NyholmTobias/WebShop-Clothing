using DreamTeam.IDP.Shared.ResponseModels;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopShared.ResponseModels;
using WebshopUI.Components;
using WebshopUI.Components.Modals;
using WebshopUI.Services.Users;

namespace WebshopUI.Pages.Admin
{
    public partial class AdminPage : ComponentBase
    {

        public List<UserResponse> Users { get; set; }

        [Inject]
        public IUserDataService UserDataService { get; set; }

        private DeleteUser DeleteUser { get; set; }

        private CreateUser CreateUser { get; set; }

        private UpdateUser UpdateUser { get; set; }

        private SomethingWentWrong SomethingWentWrong { get; set; }




        protected override async Task OnInitializedAsync()
        {
            Users = await UserDataService.GetAllUsers();
        }


        public async void DeleteModal_OnDialogClose()
        {
            Users = await UserDataService.GetAllUsers();
            StateHasChanged();
        }
        public async void CreateModal_OnDialogClose()
        {
            Users = await UserDataService.GetAllUsers();
            StateHasChanged();
        }


        public async void UpdateModal_OnDialogClose()
        {
            Users = await UserDataService.GetAllUsers();
            StateHasChanged();
        }

        public async void InfoModal_OnDialogClose()
        {
            Users = await UserDataService.GetAllUsers();
            StateHasChanged();
        }
        public void ShowDeleteModal(UserResponse user)
        {
            DeleteUser.Show(user);
        }

        public void ShowCreateModal()
        {
            CreateUser.Show();
        }

        public void ShowUpdateModal(string UserId, string Username)
        {
            UpdateUser.Show(UserId, Username);
        }

        

    }
}
