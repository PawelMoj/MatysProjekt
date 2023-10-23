using MatysProjekt.Entity;
using MatysProjekt.Entity.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using System.Security.Principal;
using Microsoft.Net.Http;
using Azure;
using Microsoft.Net.Http.Headers;

namespace MatysProjekt.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private IHttpContextAccessor httpContext;
        private EntityDbContext context;
        public UserController(EntityDbContext dbContext, IHttpContextAccessor httpContext)
        {
            this.context = dbContext;
            this.httpContext = httpContext;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserModel user)
        {
            
            if (this.context.Users != null && this.context.Users.Any(x => x.Name == user.Name && x.EncryptedPassword == user.EncryptedPassword))
            {
                var claims = new List<Claim>
                {
                    new Claim("username", user.Name),
                    new Claim(ClaimTypes.NameIdentifier, user.Name),
                    new Claim(ClaimTypes.Email, user.Email)
                };
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                await httpContext.HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    claimsPrincipal,
                    new AuthenticationProperties { IsPersistent = true });
                var result = httpContext.HttpContext?.Response.GetTypedHeaders().SetCookie;
                
                return Ok();
            }

            return BadRequest("user not exist in data base"); 
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
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        { 
            await HttpContext.SignOutAsync();
            return Ok();
        }

    }
}
