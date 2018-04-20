using System;

namespace EduHubLibrary.Facades.Views.GroupViews
{
    public class MessageView
    {
        public MessageView(int id, int senderId, string senderName, DateTimeOffset sentOn, string text)
        {
            Id = id;
            SenderId = senderId;
            SenderName = senderName;
            SentOn = sentOn;
            Text = text;
        }

        public int Id { get; }
        public int SenderId { get; }
        public string SenderName { get; }
        public DateTimeOffset SentOn { get; }
        public string Text { get; }
    }
}