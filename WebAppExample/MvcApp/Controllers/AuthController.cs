using Microsoft.AspNetCore.Mvc;

namespace MvcWebApp.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}