using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models
{
    public class NotifyResponse
    {
        public NotifyResponse(string text, DateTime date)
        {
            Text = text;
            Date = date;
        }

        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}
