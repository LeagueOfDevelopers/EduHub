using System.ComponentModel.DataAnnotations;

namespace EduHub.Models
{
    public class ReviewRequest
    {
        [Required] public string Title { get; set; }

        [Required] public string Text { get; set; }
    }
}