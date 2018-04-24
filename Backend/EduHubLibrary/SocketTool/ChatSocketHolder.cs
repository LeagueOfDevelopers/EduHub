using System.Net.WebSockets;

namespace EduHubLibrary.SocketTool
{
    public class ChatSocketHolder
    {
        public string Id { get; internal set; }
        public WebSocket Socket { get; internal set; }
        public int UserId { get; internal set; }

        public ChatSocketHolder(string id, WebSocket socket, int userId)
        {
            Id = id;
            Socket = socket;
            UserId = userId;
        }
    }
}
