using System.ComponentModel.DataAnnotations;

namespace EduHub.Models
{
    public class SearchOfUserRequest
    {
        [Required] public string Name { get; set; }
    }
}