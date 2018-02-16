using System;

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