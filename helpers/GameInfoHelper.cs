using System.Collections.Concurrent;

namespace MatysProjekt.helpers
{
    public static class GameInfoHelper
    {
        public static ConcurrentDictionary<Guid, bool> games = new ConcurrentDictionary<Guid, bool>();
        public static ConcurrentDictionary<Guid, string> gameSessions = new ConcurrentDictionary<Guid, string>();
    }
}
