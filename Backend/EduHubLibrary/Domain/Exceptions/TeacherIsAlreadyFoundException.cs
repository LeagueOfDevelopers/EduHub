using System;
using System.Runtime.Serialization;

namespace EduHubLibrary.Domain.Exceptions
{
    public class TeacherIsAlreadyFoundException : Exception
    {
        public TeacherIsAlreadyFoundException()
        {
        }

        public TeacherIsAlreadyFoundException(Guid groupId) : base($"Teacher in group {groupId} is already approved")
        {
        }

        public TeacherIsAlreadyFoundException(string message) : base(message)
        {
        }

        public TeacherIsAlreadyFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TeacherIsAlreadyFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}