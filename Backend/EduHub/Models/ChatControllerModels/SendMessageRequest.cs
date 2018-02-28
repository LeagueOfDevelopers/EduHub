using System.ComponentModel.DataAnnotations;

namespace EduHub.Models
{
    public class SendMessageRequest
    {
        /// <summary>Text of message</summary>
        [Required]
        public string Text { get; set; }
    }
}