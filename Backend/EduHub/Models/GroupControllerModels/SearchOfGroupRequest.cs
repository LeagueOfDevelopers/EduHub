using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EduHub.Models.GroupControllerModels
{
    public class SearchOfGroupRequest
    {
        [Required]
        public List<string> Tags { get; set; }
    }
}