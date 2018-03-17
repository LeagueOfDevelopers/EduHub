using System;
using System.ComponentModel.DataAnnotations;

namespace EduHub.Models
{
    public class RegistrationRequest
    {
        [Required]
        [StringLength(20, MinimumLength = 2)]
        public string Name { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Email { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 6)]
        public string Password { get; set; }

        [Required] public bool IsTeacher { get; set; }

        public int InviteCode { get; set; }
    }
}