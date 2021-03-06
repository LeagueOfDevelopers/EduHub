﻿using System;
using System.Runtime.Serialization;

namespace EduHubLibrary.Domain.Exceptions
{
    public class NotSupportedFileException : Exception
    {
        public NotSupportedFileException()
        {
        }

        public NotSupportedFileException(string message) : base(message)
        {
        }

        public NotSupportedFileException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected NotSupportedFileException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}