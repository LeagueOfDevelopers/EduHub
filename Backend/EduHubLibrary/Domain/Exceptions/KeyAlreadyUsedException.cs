using System;
using System.Runtime.Serialization;

namespace EduHubLibrary.Domain.Exceptions
{
    public class KeyAlreadyUsedException : Exception
    {
        public KeyAlreadyUsedException()
        {
        }

        public KeyAlreadyUsedException(string message) : base(message)
        {
        }

        public KeyAlreadyUsedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected KeyAlreadyUsedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}