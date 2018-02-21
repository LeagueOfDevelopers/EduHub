using System;
using System.Runtime.Serialization;

namespace EduHubLibrary.Domain.Exceptions
{
    public class CourseNotOfferedException : Exception
    {
        public CourseNotOfferedException()
        {
        }

        public CourseNotOfferedException(string message) : base(message)
        {
        }

        public CourseNotOfferedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected CourseNotOfferedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}