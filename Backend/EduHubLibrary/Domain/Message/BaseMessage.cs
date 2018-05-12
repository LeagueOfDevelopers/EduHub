using EduHubLibrary.Domain.Message;
using System;

namespace EduHubLibrary.Domain.Tools
{
    public abstract class BaseMessage
    {
        protected BaseMessage()
        {
            //Id = IntIterator.GetNextId();
            SentOn = DateTimeOffset.Now;
        }

        protected BaseMessage(int id, DateTimeOffset sentOn)
        {
            Id = id;
            SentOn = sentOn;
        }

        internal abstract MessageType GetMessageType();

        public int Id { get; internal set; }
        public DateTimeOffset SentOn { get; }
    }
}