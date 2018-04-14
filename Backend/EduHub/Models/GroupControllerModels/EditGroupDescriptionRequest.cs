using System.ComponentModel.DataAnnotations;

namespace EduHub.Models
{
    public class EditGroupDescriptionRequest
    {
        [Required]
        [StringLength(3000, MinimumLength = 20)]
        public string GroupDescription { get; set; }
    }
}