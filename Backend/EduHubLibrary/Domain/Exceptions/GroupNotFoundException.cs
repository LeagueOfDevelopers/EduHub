using System;
using System.Runtime.Serialization;

namespace EduHubLibrary.Domain.Exceptions
{
    public class GroupNotFoundException : Exception
    {
        public GroupNotFoundException(int groupId)
            : base($"Group with id {groupId} not found")
        {
        }

        public GroupNotFoundException()
        {
        }

        public GroupNotFoundException(string message) : base(message)
        {
        }

        public GroupNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        protected GroupNotFoundException(SerializationInfo info,
            StreamingContext context)
        {
        }
    }
}