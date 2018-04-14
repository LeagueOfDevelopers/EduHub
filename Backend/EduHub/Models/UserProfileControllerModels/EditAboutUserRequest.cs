using System.ComponentModel.DataAnnotations;

namespace EduHub.Models
{
    public class EditAboutUserRequest
    {
        [Required]
        [StringLength(3000, MinimumLength = 20)]
        public string AboutUser { get; set; }
    }
}