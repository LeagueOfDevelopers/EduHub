using System.ComponentModel.DataAnnotations;
using EduHubLibrary.Domain.Tools;

namespace EduHub.Models
{
    public class EditUserGenderRequest
    {
        [Required] public Gender Gender { get; set; }
    }
}