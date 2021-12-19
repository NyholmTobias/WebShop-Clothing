using DreamTeam.IDP.Shared.RequestModels;
using Microsoft.AspNetCore.Components;
using System.Threading.Tasks;
using System.Timers;
using TabBlazor.Services;
using WebshopUI.Services.Users;

namespace WebshopUI.Pages.Register
{
    public partial class RegisterPage : ComponentBase
    {
        public UserRequest User { get; set; } =
          new UserRequest { };

        [Inject]
        public IUserDataService UserDataService { get; set; }
        [Inject]
        public ToastService ToastService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        public bool ShowDialog { get; set; }

        [Parameter]
        public EventCallback<bool> CloseEventCallback { get; set; }

        
        protected async Task HandleValidSubmit()
        {
            var userResponse = await UserDataService.CreateUser(User);
            if (userResponse != null)
            {
                await ToastService.AddToastAsync(new TabBlazor.ToastModel { Title = "Success!", Message = $"{userResponse.UserName} was created!"});
                await ToastService.AddToastAsync(new TabBlazor.ToastModel { Title = "Redirecting..", Message = $"You are being redirected..." });

                var timer = new Timer { Interval = 1500, AutoReset = false };
                timer.Elapsed += new ElapsedEventHandler(AfterCreatedUser);
                timer.Start();
            }
            else
            {
                await ToastService.AddToastAsync(new TabBlazor.ToastModel { Title = "Error!", Message = $"Something went wrong!" });
            }

        }

        private void AfterCreatedUser(object sender, ElapsedEventArgs e)
        {

            NavigationManager.NavigateTo("/");
        }
    }
}
