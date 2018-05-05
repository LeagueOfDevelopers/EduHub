using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EduHubLibrary.Domain.Exceptions
{
    public class KeyNotFoundException : Exception
    {
        public KeyNotFoundException()
        {
        }

        public KeyNotFoundException(int keyId) : base($"key {keyId} not found")
        {
        }

        public KeyNotFoundException(string message) : base(message)
        {
        }

        public KeyNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected KeyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
