using Microsoft.AspNetCore.Mvc;

namespace MatysProjekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return Ok();
        }

    }
}
