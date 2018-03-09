using System;
using System.Runtime.Serialization;

namespace EduHubLibrary.Domain.Exceptions
{
    public class ReviewAlreadyAddedException : Exception
    {
        public ReviewAlreadyAddedException()
        {
        }

        public ReviewAlreadyAddedException(int userId, int teacherId)
            : base($"user with id {userId} is has already added review to teacher {teacherId}")
        {
        }

        public ReviewAlreadyAddedException(string message) : base(message)
        {
        }

        public ReviewAlreadyAddedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected ReviewAlreadyAddedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}