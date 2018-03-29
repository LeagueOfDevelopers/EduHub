using System;

namespace EduHubLibrary.Domain.Tools
{
    public class Message
    {
        public Message(int senderId, string text)
        {
            //Id = IntIterator.GetNextId();
            SenderId = senderId;
            Text = text;
            SentOn = DateTimeOffset.Now;
        }

        internal Message(int id, int senderId, DateTimeOffset sentOn, string text)
        {
            Id = id;
            SenderId = senderId;
            SentOn = sentOn;
            Text = text;
        }

        public int Id { get; internal set; }
        public int SenderId { get; }
        public DateTimeOffset SentOn { get; }
        public string Text { get; internal set; }
    }
}