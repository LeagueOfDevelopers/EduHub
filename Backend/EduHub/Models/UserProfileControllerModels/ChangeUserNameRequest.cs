using System.ComponentModel.DataAnnotations;

namespace EduHub.Models
{
    public class ChangeUserNameRequest
    {
        [Required]
        [StringLength(70, MinimumLength = 3)]
        [RegularExpression(@"[a-zA-Zа-яА-Я\s]+")]
        public string UserName { get; set; }
    }
}