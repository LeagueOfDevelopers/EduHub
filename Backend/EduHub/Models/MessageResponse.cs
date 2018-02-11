using System;

namespace EduHub.Models
{
    public class MessageResponse
    {
        public string Text { get; set; }
        public string NameSender { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}