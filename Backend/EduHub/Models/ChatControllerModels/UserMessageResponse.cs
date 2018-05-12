using EduHubLibrary.Domain.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models.ChatControllerModels
{
    public class UserMessageResponse
    {
        public UserMessageResponse(int id, DateTimeOffset sentOn, MessageType messageType, int senderId, string senderName, string text)
        {
            Id = id;
            SentOn = sentOn;
            MessageType = messageType;
            SenderId = senderId;
            SenderName = senderName;
            Text = text;
        }

        public int SenderId { get; }
        public string SenderName { get; }
        public string Text { get; }
        public int Id { get; }
        public DateTimeOffset SentOn { get; }
        public MessageType MessageType { get; }
    }
}
