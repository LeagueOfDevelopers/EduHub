using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models
{
    public class SearchOfGroupRequest
    {
        [Required]
        public List<string> Tags { get; set; }
    }
}
