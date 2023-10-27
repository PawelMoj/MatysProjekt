using System.Net.WebSockets;

namespace MatysProjekt.service
{
    public interface IGameInfoService
    {
        void AddToGames(string key, int value);
        void AddToGameSessions(string key, string value);

        Task AddUser(WebSocket socket, string userName);
    }
}
