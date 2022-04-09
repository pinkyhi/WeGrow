using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WeGrow.Client.Pages
{
    public class LogoutModel : PageModel
    {
        private readonly IConfiguration configuration;

        public LogoutModel(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            return SignOut(
                new AuthenticationProperties
                {
                    RedirectUri = configuration["appUrl"]
                },
                OpenIdConnectDefaults.AuthenticationScheme,
                CookieAuthenticationDefaults.AuthenticationScheme);
        }
    }
}
