using System;

namespace EduHub.Models
{
    public class MessageInfoResponse
    {
        public MessageInfoResponse(int id, int senderId, string senderName, DateTimeOffset sentOn, string text)
        {
            Id = id;
            SenderId = senderId;
            SenderName = senderName;
            SentOn = sentOn;
            Text = text;
        }

        public int Id { get; set; }
        public int SenderId { get; set; }
        public string SenderName { get; set; }
        public DateTimeOffset SentOn { get; set; }
        public string Text { get; set; }
    }
}