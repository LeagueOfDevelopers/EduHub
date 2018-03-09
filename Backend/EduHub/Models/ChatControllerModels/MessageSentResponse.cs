using System;

namespace EduHub.Models
{
    public class MessageSentResponse
    {
        public MessageSentResponse(int messageId)
        {
            MessageId = messageId;
        }

        public int MessageId { get; }
    }
}