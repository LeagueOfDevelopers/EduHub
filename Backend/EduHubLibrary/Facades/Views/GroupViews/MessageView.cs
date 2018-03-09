using System;

namespace EduHubLibrary.Facades.Views.GroupViews
{
    public class MessageView
    {
        public MessageView(int id, int senderId, DateTimeOffset sentOn, string text)
        {
            Id = id;
            SenderId = senderId;
            SentOn = sentOn;
            Text = text;
        }

        public int Id { get; }
        public int SenderId { get; }
        public DateTimeOffset SentOn { get; }
        public string Text { get; }
    }
}