using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EduHubLibrary.Domain.Exceptions
{
    public class AlreadyInvitedException : Exception
    {
        public AlreadyInvitedException()
        {
        }

        public AlreadyInvitedException(Guid userId, Guid groupId) 
            : base($"user with id {userId} is already invited to group with id {groupId}")
        {
        }

        public AlreadyInvitedException(string message) : base(message)
        {
        }

        public AlreadyInvitedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AlreadyInvitedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
