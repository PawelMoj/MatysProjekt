using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Net.WebSockets;


namespace MatysProjekt.service
{
    public class GameInfoService : IGameInfoService
    {
        private const string startNewGameMessage = "startNewGame";
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
                        if(v.Count == 2)
                        {
                            StartNewGame(v);
                        }
                        return v;
                    });
                }

                while (socket.State == WebSocketState.Open)
                {
                    var buffer = new byte[64];
                    WebSocketReceiveResult socketResponse;
                    var package = new List<byte>();
                    do
                    {
                        socketResponse = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                        package.AddRange(new ArraySegment<byte>(buffer, 0, socketResponse.Count));
                    } 
                    while (!socketResponse.EndOfMessage);

                    var bufferAsString = System.Text.Encoding.ASCII.GetString(package.ToArray());
                    if (!string.IsNullOrEmpty(bufferAsString))
                    {
                        var key = gameRoom.FirstOrDefault(x => x.Value.Contains(socket)).Key;
                        gameRoom.TryGetValue(key, out var sockets);
                        sockets.Remove(socket);
                        var message = new SocketMessageModel() { Payload = bufferAsString , MessageType = "move"};
                        await this.Send(message.Serialize(), sockets.ToArray());
                    }
                }
                await socket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);


            }
            catch(Exception ex) 
            {
              
            }
        }

        private async void StartNewGame(List<WebSocket> sockets)
        {
            if (sockets.Count == 2)
            {
                var message = new SocketMessageModel() { MessageType = startNewGameMessage, Payload = "X" };
                await this.Send(message.Serialize(), sockets[0]);
                message.Payload = "O";
                await this.Send(message.Serialize(), sockets[1]);
            }
        }
        
        private async Task Send(string message,params WebSocket[] socketsToSendTo)
        {
            var sockets = socketsToSendTo.Where(s => s.State == WebSocketState.Open);
            foreach (var theSocket in sockets)
            {
                var stringAsBytes = System.Text.Encoding.ASCII.GetBytes(message);
                var byteArraySegment = new ArraySegment<byte>(stringAsBytes, 0, stringAsBytes.Length);
                await theSocket.SendAsync(byteArraySegment, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }


        class SocketMessageModel
        { 
            public string MessageType { get; set; }
            public string Payload { get; set; }

            public string Serialize()
            {
                return JsonConvert.SerializeObject(this);
            }
        }
    }
}
