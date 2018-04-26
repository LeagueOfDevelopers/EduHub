using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models.Tools
{
    public class SanctionModel
    {
        [Required]
        public string BrokenRule { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public SanctionType SanctionType { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
    }
}
