using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using System.Threading.Tasks;
using WebshopUI.Services.Tokens;

namespace WebshopUI.Components
{
    public partial class LoginLogoutButton : ComponentBase
    {
        
            public AuthenticationState Identity { get; set; }
            [Inject]
            public TokenProvider TokenProvider { get; set; }
            [Inject]
            public AuthenticationStateProvider AuthenticationStateProvider { get; set; }
            [Inject]
            public NavigationManager NavigationManager { get; set; }

            protected override async Task OnInitializedAsync()
            {
                Identity = await AuthenticationStateProvider.GetAuthenticationStateAsync();

            }
            
            public void RedirectToLogin()
            {
                NavigationManager.NavigateTo("login", true);
            }
        public void RedirectToLogout()
        {
            NavigationManager.NavigateTo("logout", true);
        }
    }

    
}
