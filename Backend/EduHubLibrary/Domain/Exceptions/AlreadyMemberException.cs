using System;
using System.Runtime.Serialization;

namespace EduHubLibrary.Domain.Exceptions
{
    public class AlreadyMemberException : Exception
    {
        public AlreadyMemberException()
        {
        }

        public AlreadyMemberException(Guid userId, Guid groupId)
            : base($"user with id {userId} is already member of group with id {groupId}")
        {
        }

        public AlreadyMemberException(string message) : base(message)
        {
        }

        public AlreadyMemberException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AlreadyMemberException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}