using System;

namespace EduHub.Models
{
    public class MessageInfoResponse
    {
        public MessageInfoResponse(Guid id, Guid senderId, DateTimeOffset sentOn, string text)
        {
            Id = id;
            SenderId = senderId;
            SentOn = sentOn;
            Text = text;
        }

        public Guid Id { get; set; }
        public Guid SenderId { get; set; }
        public DateTimeOffset SentOn { get; set; }
        public string Text { get; set; }
    }
}