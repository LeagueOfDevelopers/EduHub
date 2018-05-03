using System;
using System.Net.WebSockets;

namespace EduHubLibrary.SocketTool
{
    public class ChatSocket
    {
        public ChatSocket(Guid id, int userId, int groupId, WebSocket socket)
        {
            Id = id;
            UserId = userId;
            GroupId = groupId;
            Socket = socket;
        }

        public Guid Id { get; set; }
        public int UserId { get; set; }
        public int GroupId { get; set; }
        public WebSocket Socket { get; set; }
    }
}