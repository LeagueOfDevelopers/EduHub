using System;

namespace EduHub.Models
{
    public class MessageInfoResponse
    {
        public MessageInfoResponse(int id, int senderId, DateTimeOffset sentOn, string text)
        {
            Id = id;
            SenderId = senderId;
            SentOn = sentOn;
            Text = text;
        }

        public int Id { get; set; }
        public int SenderId { get; set; }
        public DateTimeOffset SentOn { get; set; }
        public string Text { get; set; }
    }
}