using Microsoft.AspNetCore.Mvc;

namespace AuthCodeFlow.Client.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}