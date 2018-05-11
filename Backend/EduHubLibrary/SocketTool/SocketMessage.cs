using System;

namespace EduHubLibrary.SocketTool
{
    public class SocketMessage
    {
        public SocketMessage(int id, int senderId,
            string senderName, DateTimeOffset sentOn, string text)
        {
            Id = id;
            SenderId = senderId;
            SenderName = senderName ?? throw new ArgumentNullException(nameof(senderName));
            SentOn = sentOn;
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }

        public int Id { get; }
        public int SenderId { get; }
        public string SenderName { get; }
        public DateTimeOffset SentOn { get; }
        public string Text { get; }
    }
}