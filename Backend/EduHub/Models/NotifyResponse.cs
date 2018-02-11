using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
