using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Exceptions
{
    public class MemberNotFoundException : System.Exception
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

        public MemberNotFoundException(string message, System.Exception inner) : base(message, inner)
        {
        }

        protected MemberNotFoundException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context)
        {
        }
    }
}
