using System;

namespace EduHubLibrary.Data.GroupDtos
{
    public class MessageDto
    {
        public MessageDto(int id, int senderId, DateTimeOffset sentOn, string text)
        {
            Id = id;
            SenderId = senderId;
            SentOn = sentOn;
            Text = text;
        }

        public MessageDto()
        {
        }

        public int Id { get; set; }
        public int SenderId { get; set; }
        public DateTimeOffset SentOn { get; set; }
        public string Text { get; set; }
    }
}