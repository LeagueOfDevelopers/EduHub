using System;
using System.Collections.Generic;

namespace EduHubLibrary.Domain
{
    public class GroupInfo
    {
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

        public Guid Id { get; }
        public string Title { get; internal set; }
        public string Description { get; internal set; }
        public string Curriculum { get; internal set; }
        public IEnumerable<string> Tags { get; internal set; }
        public GroupType GroupType { get; }
        public bool IsPrivate { get; }
        public bool IsActive { get; set; }
        public int Size { get; internal set; }
        public double MoneyPerUser { get; internal set; }
    }
}