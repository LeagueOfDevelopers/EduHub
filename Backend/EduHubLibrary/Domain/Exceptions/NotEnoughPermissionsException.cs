using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Exceptions
{
    public class NotEnoughPermissionsException : System.Exception
    {
        public NotEnoughPermissionsException(Guid userId) 
            : base($"Member with id {userId} hasn't enough permissions to this action")
        {
        }
        public NotEnoughPermissionsException()
        {
        }

        public NotEnoughPermissionsException(string message) : base(message)
        {
        }

        public NotEnoughPermissionsException(string message, System.Exception inner) : base(message, inner)
        {
        }

        protected NotEnoughPermissionsException(System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context)
        {
        }
    }
}
