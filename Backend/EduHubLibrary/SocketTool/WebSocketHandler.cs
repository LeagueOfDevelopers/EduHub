using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EduHubLibrary.SocketTool
{
    public abstract class WebSocketHandler
    {
        public WebSocketHandler(WebSocketConnectionManager webSocketConnectionManager)
        {
            WebSocketConnectionManager = webSocketConnectionManager;
        }

        protected WebSocketConnectionManager WebSocketConnectionManager { get; set; }

        public virtual async Task OnConnected(WebSocket socket, int userId)
        {
            WebSocketConnectionManager.AddSocket(socket, userId);
        }

        public virtual async Task OnDisconnected(WebSocket socket)
        {
            await WebSocketConnectionManager.RemoveSocket(WebSocketConnectionManager.GetId(socket));
        }

        public async Task SendMessageAsync(WebSocket socket, string message)
        {
            if (socket.State != WebSocketState.Open)
                return;

            try
            {
                await socket.SendAsync(new ArraySegment<byte>(Encoding.ASCII.GetBytes(message),
                        0,
                        message.Length),
                    WebSocketMessageType.Text,
                    true,
                    CancellationToken.None);
            }
            catch
            {
                await OnDisconnected(socket);
            }
        }

        public async Task SendMessageAsync(string socketId, string message)
        {
            await SendMessageAsync(WebSocketConnectionManager.GetSocketById(socketId), message);
        }

        public async Task SendMessageToAllAsync(string message, int groupId, IEnumerable<int> userId)
        {
            var msg = new SocketMessage(message, groupId);
            foreach (var pair in WebSocketConnectionManager.GetAll(userId))
                if (pair.Socket.State == WebSocketState.Open)
                    await SendMessageAsync(pair.Socket, JsonConvert.SerializeObject(msg));
        }
    }
}