using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace EduHubLibrary.Domain.Exceptions
{
    public class InvitationAlreadyChangedException : Exception
    {
        public InvitationAlreadyChangedException()
        {
        }

        public InvitationAlreadyChangedException(Guid invitationId)
            : base($"Invitation with id {invitationId} already changed")
        {
        }

        public InvitationAlreadyChangedException(string message) : base(message)
        {
        }

        public InvitationAlreadyChangedException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected InvitationAlreadyChangedException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
