using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EduHubLibrary.Domain.Exceptions
{
    public class GroupIsNotActiveException : Exception
    {
        public GroupIsNotActiveException(Guid groupId) : base($"Grouup with id {groupId} is not active")
        {
        }

        public GroupIsNotActiveException(string message) : base(message)
        {
        }

        public GroupIsNotActiveException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GroupIsNotActiveException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
