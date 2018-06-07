using System;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using EduHub.Extensions;
using EduHubLibrary.SocketTool;
using Microsoft.AspNetCore.Http;

namespace EduHub.SocketTool
{
    public class WebSocketManagerMiddleware
    {
        private readonly RequestDelegate _next;

        public WebSocketManagerMiddleware(RequestDelegate next,
            WebSocketHandler webSocketHandler)
        {
            _next = next;
            _webSocketHandler = webSocketHandler;
        }

        private WebSocketHandler _webSocketHandler { get; }

        public async Task Invoke(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
                return;

            var token = context.Request.Query["token"].ToString();
            var userId = token.GetUserId();

            if (userId == 0)
                return;

            var socket = await context.WebSockets.AcceptWebSocketAsync();
            _webSocketHandler.OnConnected(socket, userId);

            await Receive(socket, async (result, buffer) =>
            {
                if (result.MessageType == WebSocketMessageType.Text)
                {
                }

                else if (result.MessageType == WebSocketMessageType.Close)
                {
                    await _webSocketHandler.OnDisconnected(socket);
                }
            });
        }

        private async Task Receive(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            var buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer),
                    CancellationToken.None);

                handleMessage(result, buffer);
            }
        }
    }
}