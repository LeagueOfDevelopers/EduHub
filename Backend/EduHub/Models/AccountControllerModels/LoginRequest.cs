using System.ComponentModel.DataAnnotations;

namespace EduHub.Models
{
    public class LoginRequest
    {
        [Required]
        [RegularExpression(@".+[@].+[.].+")]
        /// <summary>
        ///     User's email
        /// </summary>
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [MaxLength(50)]
        /// <summary>
        ///     User's password
        /// </summary>
        public string Password { get; set; }
    }
}