using EduHubLibrary.Domain.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using EduHubLibrary.Domain.Message;

namespace EduHubLibrary.Domain
{
    public class UserMessage : BaseMessage
    {
        public UserMessage(int senderId, string text)
        {
            SenderId = senderId;
            Text = text;
        }

        public UserMessage(int senderId, string text, int id, DateTimeOffset sentOn)
            : base(id, sentOn)
        {
            SenderId = senderId;
            Text = text;
        }

        public int SenderId { get; }
        public string Text { get; internal set; }

        internal override MessageType GetMessageType()
        {
            return MessageType.UserMessage;
        }
    }
}
