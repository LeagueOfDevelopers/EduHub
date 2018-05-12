using System;
using EduHubLibrary.Domain.Message;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Facades.Views.GroupViews
{
    public class UserMessageView : BaseMessageView
    {
        public UserMessageView(int senderId, string senderName, string text, int id, DateTimeOffset sentOn)
            : base(id, sentOn)
        {
            SenderId = senderId;
            SenderName = senderName;
            Text = text;
            MessageType = MessageType.UserMessage;
        }

        public int SenderId { get; }
        public string SenderName { get; }
        public string Text { get; }
    }
}
