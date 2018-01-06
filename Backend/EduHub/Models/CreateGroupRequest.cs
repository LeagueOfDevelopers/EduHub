using EduHubLibrary.Domain;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace EduHub.Models
{
    /// <summary>Request for creating new group</summary>
    public class CreateGroupRequest
    {
        /// <summary>Group title</summary>
        [Required]
        public string Title { get; set; }
        /// <summary>Group description</summary>
        [Required]
        public string Description { get; set; }
        /// <summary>Tags for new group(needed for search)</summary>
        [Required]
        public List<string> Tags { get; set; }
        /// <summary>Should be >= 3</summary>
        [Required]
        public int Size { get; set; }
        /// <summary>Money paid by one user</summary>
        [Required]
        public double MoneyPerUser { get; set; }
        /// <summary>Type of new group</summary>
        [Required]
        public GroupType GroupType { get; set; }
        /// <summary>visible or not in search</summary>
        [Required]
        public bool IsPrivate { get; set; }
    }
}
