using System;
using System.Runtime.Serialization;

namespace EduHubLibrary.Domain.Exceptions
{
    public class CourseNotAcceptedException : Exception
    {
        public CourseNotAcceptedException()
        {
        }

        public CourseNotAcceptedException(string message) : base(message)
        {
        }

        public CourseNotAcceptedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CourseNotAcceptedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}