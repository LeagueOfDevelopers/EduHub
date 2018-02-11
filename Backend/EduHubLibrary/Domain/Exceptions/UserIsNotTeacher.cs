using System;
using System.Runtime.Serialization;

namespace EduHubLibrary.Domain.Exceptions
{
    public class UserIsNotTeacher : Exception
    {
        public UserIsNotTeacher()
        {
        }

        public UserIsNotTeacher(Guid userId) : base($"User with id {userId} is not teacher")
        {
        }

        public UserIsNotTeacher(string message) : base(message)
        {
        }

        public UserIsNotTeacher(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected UserIsNotTeacher(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}