using System;
using System.Runtime.Serialization;

namespace EduHubLibrary.Domain.Exceptions
{
    public class AlreadyTeacherException : Exception
    {
        public AlreadyTeacherException()
        {
        }

        public AlreadyTeacherException(int userId)
            : base($"user with id {userId} is already teacher")
        {
        }

        public AlreadyTeacherException(string message) : base(message)
        {
        }

        public AlreadyTeacherException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AlreadyTeacherException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}