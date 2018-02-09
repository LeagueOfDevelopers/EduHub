using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EduHubLibrary.Domain.Exceptions
{
    public class FileNotFoundException : Exception
    {
        public FileNotFoundException()
        {
        }

        public FileNotFoundException(string filename) : base($"file with title {filename} not found")
        {
        }

        public FileNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected FileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
