using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Http;
using System.Globalization;
using System.Threading;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Server.IIS;

namespace BlazorApp1.Controllers
{
    [Route("[controller]/[action]")]
    [Authorize(AuthenticationSchemes = IISServerDefaults.AuthenticationScheme, Roles = "valami")]
    public class CultureController : Controller
    {
        public IActionResult SetCulture(string culture, string redirectUri)
        {
            if (culture != null)
            {
                HttpContext.Response.Cookies.Append(
                    CookieRequestCultureProvider.DefaultCookieName,
                    CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture, culture)));
            }

            return LocalRedirect(redirectUri);
        }

        public IActionResult ResetCulture(string redirectUri)
        {
            HttpContext.Response.Cookies.Delete(CookieRequestCultureProvider.DefaultCookieName);

            return LocalRedirect(redirectUri);
        }
    }
}
