using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace EduHubLibrary.SocketTool
{
    public class WebSocketConnectionManager
    {
        private List<ChatSocketHolder> _chatSockets = new List<ChatSocketHolder>();

        public WebSocket GetSocketById(string id)
        {
            return _chatSockets.FirstOrDefault(p => p.Id == id).Socket;
        }

        public IEnumerable<ChatSocketHolder> GetAll(IEnumerable<int> userId)
        {
            var users = userId.ToList();
            return _chatSockets.Where(p => users.Any(c => c == p.UserId));
        }

        public string GetId(WebSocket socket)
        {
            return _chatSockets.FirstOrDefault(p => p.Socket == socket).Id;
        }
        public void AddSocket(WebSocket socket, int userId)
        {
            _chatSockets.Add(new ChatSocketHolder(CreateConnectionId(), socket, userId));
        }

        public async Task RemoveSocket(string id)
        {
            var socket = _chatSockets.Find(p => p.Id == id).Socket;
            _chatSockets.RemoveAll(p => p.Socket == socket);

            await socket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure,
                statusDescription: "Closed by the WebSocketManager",
                cancellationToken: CancellationToken.None);

        }

        private string CreateConnectionId()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
