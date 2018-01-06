using EduHubLibrary.Common;
using System.ComponentModel.DataAnnotations;

namespace EduHub.Models
{
    public class RegistrationRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public bool IsTeacher { get; set; }
        public string AvatarLink { get; set; }
        public string InviteCode { get; set; }
    }
}
