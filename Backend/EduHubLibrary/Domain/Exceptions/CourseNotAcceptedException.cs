using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

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
