using System.ComponentModel.DataAnnotations;
using EduHubLibrary.Domain;

namespace EduHub.Models
{
    public class EditGroupTypeRequest
    {
        [Required]
        public GroupType GroupType { get; set; }
    }
}