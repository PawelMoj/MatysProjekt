using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace MatysProjekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : Controller
    {        
        private ConcurrentDictionary<Guid, bool> games;
        private ConcurrentDictionary<Guid, string> gameSessions;
        private IHttpContextAccessor http;
        public GameController(IHttpContextAccessor http)
        {
            this.http = http;
            games = new ConcurrentDictionary<Guid, bool>();
            gameSessions = new ConcurrentDictionary<Guid, string>();
        }

        
        [HttpPost("getOponent")]
        public IActionResult GetNewOponent()
        {
            try
            {
                var session = HttpContext.Session.Id;
                if (games.Any(x => x.Value == false))
                {
                    var gameKey = games.FirstOrDefault(x => x.Value == false).Key;
                    games.AddOrUpdate(gameKey, false, (k, v) => v = !v);
                    gameSessions.TryGetValue(gameKey, out string lastSession);
                    gameSessions.TryUpdate(gameKey, string.Join("+", lastSession, session), lastSession);
                }
                else
                {
                    var guid = Guid.NewGuid();
                    games.TryAdd(guid, false);
                    gameSessions.TryAdd(guid, session);
                }
                return Ok();
            }
            catch
            {
                return BadRequest("something went wrong");
            }
        }
    }
}
