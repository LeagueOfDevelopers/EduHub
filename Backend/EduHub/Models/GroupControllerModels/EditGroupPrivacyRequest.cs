using System.ComponentModel.DataAnnotations;

namespace EduHub.Models
{
    public class EditGroupPrivacyRequest
    {
        [Required]
        public bool IsPrivate { get; set; }
    }
}