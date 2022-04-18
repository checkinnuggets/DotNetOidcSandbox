using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SecuredWebApp.Models;

namespace SecuredWebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var viewModel = new IndexViewModel();
            viewModel.Name = HttpContext.User.FindFirst("name").Value;
            viewModel.Greeting = await FetchGreeting();

            return View(viewModel);
        }


        private async Task<string> FetchGreeting()
        {
            var accessToken = await HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            var content = await client.GetStringAsync("https://localhost:44301/greetings");
            return content;
        }
    }
}
