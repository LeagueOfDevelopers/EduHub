using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using EduHubLibrary.SocketTool;

namespace EduHub.Middleware
{
    public class NotificationsMessageHandler : WebSocketHandler
    {
        public NotificationsMessageHandler(WebSocketConnectionManager webSocketConnectionManager) : base(webSocketConnectionManager)
        {
        }
    }
}
