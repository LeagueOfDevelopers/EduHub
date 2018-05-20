using System;
using EduHubLibrary.Domain.Message;

namespace EduHubLibrary.Facades.Views.GroupViews
{
    public abstract class BaseMessageView
    {
        protected BaseMessageView(int id, DateTimeOffset sentOn)
        {
            Id = id;
            SentOn = sentOn;
        }

        public int Id { get; }
        public DateTimeOffset SentOn { get; }
        public MessageType MessageType { get; protected set; }
    }
}