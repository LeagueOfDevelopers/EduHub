using System.ComponentModel.DataAnnotations;

namespace EduHub.Models
{
    public class RegistrationRequest
    {
        [Required]
        [StringLength(70, MinimumLength = 3)]
        [RegularExpression(@"[a-zA-Zа-яА-Я\s]+")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@".+[@].+[.].+")]
        public string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 8)]
        public string Password { get; set; }

        [Required] public bool IsTeacher { get; set; }

        public int InviteCode { get; set; }
    }
}