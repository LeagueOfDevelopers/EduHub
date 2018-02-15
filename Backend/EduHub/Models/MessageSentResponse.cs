using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models
{
    public class MessageSentResponse
    {
        public MessageSentResponse(Guid messageId)
        {
            MessageId = messageId;
        }

        public Guid MessageId { get; }
    }
}
