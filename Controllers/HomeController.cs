using MatysProjekt.Entity;
using MatysProjekt.Entity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MatysProjekt.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        private EntityDbContext context;

        public HomeController(EntityDbContext context) 
        {
            this.context = context;
        }
        public IEnumerable<ProductModel> GetProducts()
        {
            return this.context.Products?.Where(x => x.ProductName.StartsWith("Rower"));
        }
        [HttpGet("getproduct/{id:int}")]
        public ProductModel GetProduct(int id) 
        { 
            return this.context.Products?.FirstOrDefault(x => x.Id == id);
        }
    }
}
