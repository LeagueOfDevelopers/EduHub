using System.ComponentModel.DataAnnotations;

namespace EduHub.Models
{
    public class EditGroupTitleRequest
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string GroupTitle { get; set; }
    }
}