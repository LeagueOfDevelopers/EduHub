using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models
{
    public class CreateGroupRequest
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public List<string> Tags { get; set; }
        [Required]
        public int Size { get; set; }
        [Required]
        public double MoneyPerUser { get; set; }
        [Required]
        public GroupType GroupType { get; set; }
        [Required]
        public bool IsPrivate { get; set; }
    }
}
