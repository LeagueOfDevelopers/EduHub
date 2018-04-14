using System.ComponentModel.DataAnnotations;

namespace EduHub.Models
{
    public class EditGroupPriceRequest
    {
        [Required]
        [Range(0, long.MaxValue)]
        public double GroupPrice { get; set; }
    }
}