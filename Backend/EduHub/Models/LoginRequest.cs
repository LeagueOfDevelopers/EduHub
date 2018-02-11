using System.ComponentModel.DataAnnotations;

namespace EduHub.Models
{
    public class LoginRequest
    {
        [Required]
        /// <summary>User's email</summary>
        public string Email { get; set; }

        [Required]
        /// <summary>User's password</summary>
        public string Password { get; set; }
    }
}