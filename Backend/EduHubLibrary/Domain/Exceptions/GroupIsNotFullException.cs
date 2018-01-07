using System;
using System.Runtime.Serialization;

namespace EduHubLibrary.Domain.Exceptions
{
    class GroupIsNotFullException : Exception
    {
        public GroupIsNotFullException()
        {
        }

        public GroupIsNotFullException(Guid idOfGroup) : base($"group with id {idOfGroup} is not full")
        {

        }

        public GroupIsNotFullException(string message) : base(message)
        {
        }

        public GroupIsNotFullException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected GroupIsNotFullException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
