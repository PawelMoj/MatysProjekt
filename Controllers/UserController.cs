using MatysProjekt.Entity;
using MatysProjekt.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace MatysProjekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private EntityDbContext context;
        public UserController(EntityDbContext dbContext)
        {
            this.context = dbContext;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserModel user)
        {
            //var loginuser = new UserModel() { Email = user.Email, EncryptedPassword = user.EncryptedPassword, LastLogonAttempt = DateTime.Now.ToString(), Name = user.Name };
            if (this.context.Users != null && this.context.Users.Any(x => x.Name == user.Name && x.EncryptedPassword == user.EncryptedPassword))
            {
                return Ok();
            }

            return BadRequest("user not exist in data base"); ;
        }


        [HttpPost("register")]
        public IActionResult Register([FromBody] UserModel user)
        {
            var dbuser = new UserModel() { Email = user.Email, EncryptedPassword = user.EncryptedPassword, LastLogonAttempt = DateTime.Now.ToString(), Name = user.Name };

            //Implementacja
            try
            {
                this.context.Users.Add(dbuser);
                this.context.SaveChanges();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest("Failure in database");
            }


        }

    }
}
