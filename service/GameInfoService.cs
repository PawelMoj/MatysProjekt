using Microsoft.AspNetCore.Mvc;
using System.Collections.Concurrent;
using System.Net.WebSockets;

namespace MatysProjekt.service
{
    public class GameInfoService : IGameInfoService
    {
        //private static ConcurrentDictionary<Guid, bool> games = new ConcurrentDictionary<Guid, bool>();
        private static ConcurrentDictionary<Guid, List<WebSocket>> gameRoom = new ConcurrentDictionary<Guid, List<WebSocket>>();
        private object gameRoomLock = new object();

        public void AddToGames(string key, int value)
        {
            throw new NotImplementedException();
        }

        public void AddToGameSessions(string key, string value)
        {
            throw new NotImplementedException();
        }

        public async Task AddUser(WebSocket socket, string userName)
        {
            try
            {
                lock (gameRoomLock)
                {
                    var gameKey = gameRoom.FirstOrDefault(x => x.Value?.Count == 1).Key;
                    if (gameKey == Guid.Empty)
                    {
                        gameKey = Guid.NewGuid();
                    }
                    gameRoom.AddOrUpdate(gameKey, new List<WebSocket>() { socket }, (k, v) =>
                    {
                        if (v == null)
                        { 
                            v = new List<WebSocket>();
                        }
                        v.Add(socket);
                        return v;
                    });
                }
              
            }
            catch(Exception ex) 
            {
              
            }
        }
    }
}
