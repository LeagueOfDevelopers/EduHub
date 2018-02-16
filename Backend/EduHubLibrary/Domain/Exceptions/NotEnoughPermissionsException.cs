using System;
using System.Runtime.Serialization;

namespace EduHubLibrary.Domain.Exceptions
{
    public class NotEnoughPermissionsException : Exception
    {
        public NotEnoughPermissionsException(Guid userId)
            : base($"User with id {userId} hasn't enough permissions to this action")
        {
        }

        public NotEnoughPermissionsException()
        {
        }

        public NotEnoughPermissionsException(string message) : base(message)
        {
        }

        public NotEnoughPermissionsException(string message, Exception inner) : base(message, inner)
        {
        }

        protected NotEnoughPermissionsException(SerializationInfo info,
            StreamingContext context)
        {
        }
    }
}