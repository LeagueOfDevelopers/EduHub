using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using EduHub.Models.ValidationAttributes;
using EduHubLibrary.Domain;

namespace EduHub.Models
{
    /// <summary>Request for creating new group</summary>
    public class CreateGroupRequest
    {
        /// <summary>Group title</summary>
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Title { get; set; }

        /// <summary>Group description</summary>
        [Required]
        [StringLength(3000, MinimumLength = 20)]
        public string Description { get; set; }

        /// <summary>Tags for new group(needed for search)</summary>
        [Required]
        [ListLength(3, 10)] public List<string> Tags { get; set; }

        [Required] [Range(1, 200)] public int Size { get; set; }

        [Required] [Range(0, long.MaxValue)] public double MoneyPerUser { get; set; }

        /// <summary>Type of new group</summary>
        [Required]
        public GroupType GroupType { get; set; }

        /// <summary>visible or not in search</summary>
        [Required]
        public bool IsPrivate { get; set; }
    }
}