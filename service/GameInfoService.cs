using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;

namespace MatysProjekt.service
{
    public class GameInfoService : IGameInfoService
    {
        private static ConcurrentDictionary<Guid, bool> games = new ConcurrentDictionary<Guid, bool>();;
        private static ConcurrentDictionary<Guid, string> gameSessions = new ConcurrentDictionary<Guid, string>();;

        public void AddToGames(string key, int value)
        {
            throw new NotImplementedException();
        }

        public void AddToGameSessions(string key, string value)
        {
            throw new NotImplementedException();
        }

        public void osamitukurwalata(string session)
        {
            try
            {
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
              
            }
            catch
            {
              
            }
        }
    }
}
