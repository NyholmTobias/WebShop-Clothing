using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace WebshopUI.Pages
{
    public class LoginIDPModel : PageModel
    {

        /// <summary>
        /// Används endast för att redirecta till login sidan
        /// </summary>
        /// <param name="redirectUri"></param>
        /// <returns></returns>
        public async Task OnGetAsync(string redirectUri)
        {
            if (string.IsNullOrWhiteSpace(redirectUri))
            {
                redirectUri = Url.Content("~/");
            }

            if (HttpContext.User.Identity.IsAuthenticated)
            {
                Response.Redirect(redirectUri);
            }

            await HttpContext.ChallengeAsync(
               OpenIdConnectDefaults.AuthenticationScheme,
               new AuthenticationProperties { RedirectUri = redirectUri });
        }
    }
}
