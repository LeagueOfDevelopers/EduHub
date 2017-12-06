using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models
{
    public class SendMessageRequest
    {
        public string Receiver { get; set; }
        public string Text { get; set; }
    }
}
