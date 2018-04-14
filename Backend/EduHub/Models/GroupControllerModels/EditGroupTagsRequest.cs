using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EduHub.Models.ValidationAttributes;

namespace EduHub.Models
{
    public class EditGroupTagsRequest
    {
        [Required]
        [ListLength(3, 10)]
        public List<string> GroupTags { get; set; }
    }
}