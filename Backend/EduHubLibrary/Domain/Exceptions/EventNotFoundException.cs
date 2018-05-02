using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

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
