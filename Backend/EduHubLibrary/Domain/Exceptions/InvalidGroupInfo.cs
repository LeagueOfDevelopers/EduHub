using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EduHubLibrary.Domain.Exceptions
{
    public class InvalidGroupInfo : Exception
    {
        public InvalidGroupInfo()
        {
        }

        public InvalidGroupInfo(string parameter)
            : base($"Parameter '{parameter}' has invalid value")
        {
        }

        public InvalidGroupInfo(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvalidGroupInfo(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
