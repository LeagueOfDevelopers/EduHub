using System;
using System.Runtime.Serialization;

namespace EduHubLibrary.Domain.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException(int userId)
            : base($"User with id {userId} not found")
        {
        }

        public UserNotFoundException()
        {
        }

        public UserNotFoundException(string message) : base(message)
        {
        }

        public UserNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        protected UserNotFoundException(SerializationInfo info,
            StreamingContext context)
        {
        }
    }
}