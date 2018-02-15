using System;

namespace EduHub.Models
{
    public class MessageResponse
    {
        public MessageResponse(string text, Guid senderId, DateTimeOffset sentOn)
        {
            Text = text;
            SenderId = senderId;
            SentOn = sentOn;
        }

        public string Text { get; set; }
        public Guid SenderId { get; set; }
        public DateTimeOffset SentOn { get; set; }
    }
}