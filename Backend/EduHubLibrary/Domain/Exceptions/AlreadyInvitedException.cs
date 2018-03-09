using System;
using System.Runtime.Serialization;

namespace EduHubLibrary.Domain.Exceptions
{
    public class AlreadyInvitedException : Exception
    {
        public AlreadyInvitedException()
        {
        }

        public AlreadyInvitedException(int userId, int groupId)
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