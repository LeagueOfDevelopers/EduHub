using System;

namespace EduHubLibrary.Facades.Views.GroupViews
{
    public class MessageView
    {
        public MessageView(Guid id, Guid senderId, DateTimeOffset sentOn, string text)
        {
            Id = id;
            SenderId = senderId;
            SentOn = sentOn;
            Text = text;
        }

        public Guid Id { get; }
        public Guid SenderId { get; }
        public DateTimeOffset SentOn { get; }
        public string Text { get; }
    }
}