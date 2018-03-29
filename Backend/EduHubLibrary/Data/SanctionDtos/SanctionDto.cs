using System;
using System.ComponentModel.DataAnnotations;
using EduHubLibrary.Domain;

namespace EduHubLibrary.Data.SanctionDtos
{
    public class SanctionDto
    {
        internal SanctionDto()
        {
        }

        [Key] public int Id { get; set; }

        public string BrokenRule { get; set; }
        public int UserId { get; set; }
        public int ModeratorId { get; set; }
        public bool IsTemporary { get; set; }
        public DateTimeOffset ExpirationDate { get; set; }
        public SanctionType Type { get; set; }
        public bool IsActive { get; set; }
    }
}