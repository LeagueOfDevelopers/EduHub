using System;
using System.Runtime.Serialization;

namespace EduHubLibrary.Domain.Exceptions
{
    public class MemberNotFoundException : Exception
    {
        public MemberNotFoundException(Guid userId)
            : base($"Member with id {userId} not found")
        {
        }

        public MemberNotFoundException()
        {
        }

        public MemberNotFoundException(string message) : base(message)
        {
        }

        public MemberNotFoundException(string message, Exception inner) : base(message, inner)
        {
        }

        protected MemberNotFoundException(SerializationInfo info,
            StreamingContext context)
        {
        }
    }
}