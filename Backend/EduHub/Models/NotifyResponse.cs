using System;

namespace EduHub.Models
{
    public class NotifyResponse
    {
        public NotifyResponse(string text, DateTimeOffset date)
        {
            Text = text;
            Date = date;
        }

        public string Text { get; set; }
        public DateTimeOffset Date { get; set; }
    }
}