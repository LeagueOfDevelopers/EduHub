using System;
using System.Runtime.Serialization;

namespace EduHubLibrary.Domain.Exceptions
{
    public class GroupIsFullException : Exception
    {
        public GroupIsFullException()
        {
        }

        public GroupIsFullException(int idOfGroup) : base($"group with id {idOfGroup} is full")
        {
        }

        public GroupIsFullException(string message) : base(message)
        {
        }

        public GroupIsFullException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GroupIsFullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}