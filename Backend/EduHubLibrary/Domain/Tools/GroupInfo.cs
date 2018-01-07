using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain
{
    public class GroupInfo
    {
        public Guid Id { get; set; }
        public string Title{ get; set; }
        public string Description { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public GroupType GroupType { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsActive { get; set; }
        public int Size { get; set; }
        public double MoneyPerUser { get; set; }

        public GroupInfo(Guid id, string title, string description, IEnumerable<string> tags, GroupType groupType, 
            bool isPrivate, bool isActive, int size, double moneyPerUser)
        {
            Id = id;
            Title = title;
            Description = description;
            Tags = tags;
            GroupType = groupType;
            IsPrivate = isPrivate;
            IsActive = isActive;
            Size = size;
            MoneyPerUser = moneyPerUser;
        }
    }
}
