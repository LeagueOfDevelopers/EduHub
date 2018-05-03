using System;
using System.ComponentModel.DataAnnotations;
using EduHubLibrary.Domain;

namespace EduHub.Models.SanctionsController
{
    public class ApplySanctionRequest
    {
        [Required] public string BrokenRule { get; set; }

        [Required] public int UserId { get; set; }

        [Required] public SanctionType SanctionType { get; set; }

        public DateTimeOffset ExpirationDate { get; set; }
    }
}