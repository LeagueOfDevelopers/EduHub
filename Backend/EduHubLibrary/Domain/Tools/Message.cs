using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Tools
{
    public class Message
    {
        public Message(Guid senderId, Guid receiverId, string text)
        {
            Id = Guid.NewGuid();
            SenderId = senderId;
            ReceiverId = receiverId;
            Text = text;
        }

        public Guid Id { get; }
        public Guid SenderId { get; }
        public Guid ReceiverId { get; }
        public string Text { get; internal set; }
    }
}
