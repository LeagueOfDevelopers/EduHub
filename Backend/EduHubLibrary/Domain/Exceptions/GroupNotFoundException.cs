using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Exceptions
{
    public class GroupNotFoundException : System.Exception
    {
        public GroupNotFoundException(Guid groupId) 
            : base($"Group with id {groupId} not found")
        {
        }
        public GroupNotFoundException()
        {
        }

        public GroupNotFoundException(string message) : base(message)
        {
        }

        public GroupNotFoundException(string message, System.Exception inner) : base(message, inner)
        {
        }

        protected GroupNotFoundException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context)
        {
        }
    }
}
