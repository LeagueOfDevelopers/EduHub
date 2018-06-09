using System.ComponentModel.DataAnnotations;

namespace EduHub.Models.AccountControllerModels
{
    public class RestorePasswordRequest
    {
        [Required]
        [RegularExpression(@".+[@].+[.].+")]
        public string Email { get; set; }
    }
}
