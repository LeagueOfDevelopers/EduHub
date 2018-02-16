using System;

namespace EduHubLibrary.Domain.Tools
{
    public class Message
    {
        public Message(Guid senderId, string text)
        {
            Id = Guid.NewGuid();
            SenderId = senderId;
            Text = text;
            SentOn = DateTimeOffset.Now;
        }

        public Guid Id { get; }
        public Guid SenderId { get; }
        public DateTimeOffset SentOn { get; }
        public string Text { get; internal set; }
    }
}