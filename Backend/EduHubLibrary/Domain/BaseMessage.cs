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

        public int Id { get; internal set; }
        public DateTimeOffset SentOn { get; }
    }
}