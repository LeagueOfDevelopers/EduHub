using System;
using EduHubLibrary.Interators;

namespace EduHubLibrary.Domain.Tools
{
    public class Message
    {
        public Message(int senderId, string text)
        {
            Id = IntIterator.GetNextId();
            SenderId = senderId;
            Text = text;
            SentOn = DateTimeOffset.Now;
        }

        public int Id { get; internal set; }
        public int SenderId { get; }
        public DateTimeOffset SentOn { get; }
        public string Text { get; internal set; }
    }
}