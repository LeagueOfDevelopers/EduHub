using System;
using System.Collections.Generic;

namespace EduHubLibrary.Domain
{
    public class GroupInfo
    {
        public GroupInfo(string title, string description, IEnumerable<string> tags, GroupType groupType,
            bool isPrivate, bool isActive, int size, double price)
        {
            Title = title;
            Description = description;
            Tags = tags;
            GroupType = groupType;
            IsPrivate = isPrivate;
            IsActive = isActive;
            Size = size;
            Price = price;
        }

        internal GroupInfo(int id, string title, string description, string curriculum, IEnumerable<string> tags,
            GroupType groupType, bool isPrivate, bool isActive, int size, double price)
        {
            Id = id;
            Title = title;
            Description = description;
            Curriculum = curriculum;
            Tags = tags;
            GroupType = groupType;
            IsPrivate = isPrivate;
            IsActive = isActive;
            Size = size;
            Price = price;
        }

        public int Id { get; internal set; }
        public string Title { get; internal set; }
        public string Description { get; internal set; }
        public string Curriculum { get; internal set; }
        public IEnumerable<string> Tags { get; internal set; }
        public GroupType GroupType { get; internal set; }
        public bool IsPrivate { get; internal set; }
        public bool IsActive { get; internal set; }
        public int Size { get; internal set; }
        public double Price { get; internal set; }
    }
}