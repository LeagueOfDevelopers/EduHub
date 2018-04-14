using System.ComponentModel.DataAnnotations;

namespace EduHub.Models
{
    public class EditGroupSizeRequest
    {
        [Required]
        [Range(1, 200)]
        public int GroupSize { get; set; }
    }
}