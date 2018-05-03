using System.ComponentModel.DataAnnotations;
using EduHub.Models.ValidationAttributes;

namespace EduHub.Models
{
    public class EditBirthYearRequest
    {
        [Required] [BirthCheck(1900)] public int BirthYear { get; set; }
    }
}