using System;
using System.ComponentModel.DataAnnotations;
using EduHub.Models.ValidationAttributes;

namespace EduHub.Models
{
    public class EditBirthYearRequest
    {
        [Required] [BirthCheck(1900)] public DateTimeOffset BirthYear { get; set; }
    }
}