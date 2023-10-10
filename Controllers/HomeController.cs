using Microsoft.AspNetCore.Mvc;

namespace MatysProjekt.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
