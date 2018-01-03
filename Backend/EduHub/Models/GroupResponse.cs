using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
namespace EduHub.Models
{
    public class GroupResponse
    {
        public GroupResponse(string name, string description, bool isActive, List<string> tags, IEnumerable<Member> members, double totalValue, int size)
        {
            Name = name;
            Description = description;
            IsActive = isActive;
            Tags = tags;
            Members = members;
            TotalValue = totalValue;
            Size = size;
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public List<string> Tags { get; set; }
        public IEnumerable<Member> Members { get; set; }
        public double TotalValue { get; set; }
        public int Size { get; set; }
        public bool isPrivate = false;
        public bool hasTeacher = false;
    }
}
