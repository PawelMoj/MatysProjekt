using MatysProjekt.service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace MatysProjekt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GameController : Controller
    {        
        private static ConcurrentDictionary<Guid, bool> games;
        private static ConcurrentDictionary<Guid, string> gameSessions;
        private IHttpContextAccessor http;
        private IGameInfoService _gameInfoService;
        public GameController(IHttpContextAccessor http, IGameInfoService gameInfoService)
        {
            this.http = http;
            _gameInfoService = gameInfoService;

            games = new ConcurrentDictionary<Guid, bool>();
            gameSessions = new ConcurrentDictionary<Guid, string>();
        }

        
        [HttpPost("getOponent")]
        public IActionResult GetNewOponent()
        {
            string session = HttpContext.Session.Id;
            _gameInfoService.osamitukurwalata(session);
            /*
            try
            {
                var session = HttpContext.Session.Id;
                if (games.Any(x => x.Value == false))
                {
                    var gameKey = games.FirstOrDefault(x => x.Value == false).Key; // to gowno globalnie static albo serwis
                    games.AddOrUpdate(gameKey, false, (k, v) => v = !v);
                    gameSessions.TryGetValue(gameKey, out string lastSession);
                    gameSessions.TryUpdate(gameKey, string.Join("+", lastSession, session), lastSession);
                }
                else
                {
                    var guid = Guid.NewGuid();
                    games.TryAdd(guid, false);
                    Console.Write(gameSessions);
                    gameSessions.TryAdd(guid, session);
                }
                return Ok();
            }
            catch
            {
                return BadRequest("something went wrong");
            }
            */
        }
        
    }
}
