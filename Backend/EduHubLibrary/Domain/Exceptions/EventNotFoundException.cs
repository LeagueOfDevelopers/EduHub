using System;
using System.Runtime.Serialization;

namespace EduHubLibrary.Domain.Exceptions
{
    public class EventNotFoundException : Exception
    {
        public EventNotFoundException(int eventId) :
            base($"Event with id {eventId} not found")
        {
        }

        public EventNotFoundException(string message) : base(message)
        {
        }

        public EventNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected EventNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}