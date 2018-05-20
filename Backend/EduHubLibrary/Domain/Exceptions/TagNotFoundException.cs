using System;
using System.Runtime.Serialization;

namespace EduHubLibrary.Domain.Exceptions
{
    public class TagNotFoundException : Exception
    {
        public TagNotFoundException()
        {
        }

        public TagNotFoundException(string tagId) : base($"tag {tagId} not found")
        {
        }

        public TagNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected TagNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}