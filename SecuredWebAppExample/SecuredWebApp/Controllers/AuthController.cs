using Microsoft.AspNetCore.Mvc;

namespace SecuredWebApp.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Logout()
        {
            return SignOut("Cookies", "oidc");
        }
    }
}