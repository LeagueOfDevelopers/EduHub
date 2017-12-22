using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models
{
    public class MessageResponse
    {
        public string Text { get; set; }
        public string NameSender { get; set; }
        public DateTime Date { get; set; }
    }
}
