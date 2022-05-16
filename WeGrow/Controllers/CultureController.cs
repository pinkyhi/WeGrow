using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;

namespace WeGrow.Controllers
{
    [Route("{controller}/[action]")]
    public class CultureController : Controller
    {
        [HttpGet]
        public IActionResult SetCulture(string culture, string redirectUri)
        {
            if (!string.IsNullOrWhiteSpace(culture))
            {
                HttpContext.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(
                        new RequestCulture(culture)));
            }

            return Redirect(redirectUri);
        }
    }
}
