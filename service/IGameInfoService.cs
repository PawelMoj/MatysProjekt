namespace MatysProjekt.service
{
    public interface IGameInfoService
    {
        void AddToGames(string key, int value);
        void AddToGameSessions(string key, string value);

        void osamitukurwalata(IHttpContextAccessor session);
    }
}
