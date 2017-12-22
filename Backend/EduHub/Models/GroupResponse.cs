using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
namespace EduHub.Models
{
    public class GroupResponse
    {
        public GroupResponse(string name, string description, bool isActive, List<string> tags)
        {
            Name = name;
            Description = description;
            IsActive = isActive;
            Tags = tags;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public List<string> Tags { get; set; }
    }
}
