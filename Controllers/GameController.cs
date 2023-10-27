using MatysProjekt.helpers;
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
        private IHttpContextAccessor http;



        // servis option 1
        private IGameInfoService _gameInfoService;
        // helper option 2

        public GameController(IHttpContextAccessor http, IGameInfoService gameInfoService)
        {
            this.http = http;
            _gameInfoService = gameInfoService;
        }

        
        [HttpPost("getOponent")]
        public IActionResult GetNewOponent()
        {
            /*
            string session = HttpContext.Session.Id;
            _gameInfoService.osamitukurwalata(session);
            return Ok();
            */

            
            try
            {
                var session = HttpContext.Session.Id;
                if (GameInfoHelper.games.Any(x => x.Value == false))
                {
                    var gameKey = GameInfoHelper.games.FirstOrDefault(x => x.Value == false).Key; // to gowno globalnie static albo serwis
                    GameInfoHelper.games.AddOrUpdate(gameKey, false, (k, v) => v = !v);
                    GameInfoHelper.gameSessions.TryGetValue(gameKey, out string lastSession);
                    GameInfoHelper.gameSessions.TryUpdate(gameKey, string.Join("+", lastSession, session), lastSession);
                }
                else
                {
                    var guid = Guid.NewGuid();
                    GameInfoHelper.games.TryAdd(guid, false);
                    Console.Write(GameInfoHelper.gameSessions);
                    GameInfoHelper.gameSessions.TryAdd(guid, session);
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
