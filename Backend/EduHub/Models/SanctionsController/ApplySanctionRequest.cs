using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models.SanctionsController
{
    public class ApplySanctionRequest
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
