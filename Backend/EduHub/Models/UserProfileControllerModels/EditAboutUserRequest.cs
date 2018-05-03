using System.ComponentModel.DataAnnotations;

namespace EduHub.Models
{
    public class EditAboutUserRequest
    {
        [StringLength(3000)] public string AboutUser { get; set; }
    }
}