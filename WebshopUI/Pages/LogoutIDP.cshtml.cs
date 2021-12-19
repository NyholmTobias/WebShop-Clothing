using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace WebshopUI.Pages
{
    /// <summary>
    /// Kallas på för att logga ut
    /// </summary>
    public class LogoutIDPModel : PageModel
    {
        
        public async Task OnGetAsync()
        {
            await HttpContext
                .SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme); //Rensar ur cookien
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme); //Loggar ut ur IDP

        }
    }
}
