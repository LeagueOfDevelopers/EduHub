using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EduHubLibrary.Domain.Exceptions
{
    public class SanctionNotFoundException : Exception
    {
        public SanctionNotFoundException(int sanctionId) : base($"Sanction with id {sanctionId} not found")
        {
        }

        public SanctionNotFoundException(string message) : base(message)
        {
        }

        public SanctionNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected SanctionNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
