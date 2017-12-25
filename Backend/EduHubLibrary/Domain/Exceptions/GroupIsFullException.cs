using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EduHubLibrary.Domain.Exceptions
{
    public class GroupIsFullException : Exception
    {
        public GroupIsFullException()
        {
        }

        public GroupIsFullException(Guid idOfGroup) : base($"group with id {idOfGroup} is full")
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
