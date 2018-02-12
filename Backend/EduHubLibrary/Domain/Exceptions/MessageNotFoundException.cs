using System;
using System.Runtime.Serialization;

namespace EduHubLibrary.Domain.Exceptions
{
    public class MessageNotFoundException : Exception
    {
        public MessageNotFoundException(Guid messageId)
            : base($"Message with id {messageId} not found")
        {
        }

        public MessageNotFoundException(string message) : base(message)
        {
        }

        public MessageNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected MessageNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}