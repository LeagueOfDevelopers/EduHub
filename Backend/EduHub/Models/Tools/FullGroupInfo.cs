using System.Collections.Generic;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Tools;

namespace EduHub.Models.Tools
{
    public class FullGroupInfo
    {
        public FullGroupInfo(string title, int size, int memberAmount, double cost, GroupType groupType,
            IEnumerable<string> tags, string description, CourseStatus courseStatus,
            bool isPrivate, string curriculum)
        {
            Title = title;
            Size = size;
            MemberAmount = memberAmount;
            Cost = cost;
            GroupType = groupType;
            Tags = tags;
            Description = description;
            CourseStatus = courseStatus;
            IsPrivate = isPrivate;
            Curriculum = curriculum;
        }

        public string Title { get; }
        public int Size { get; }
        public int MemberAmount { get; }
        public double Cost { get; }
        public GroupType GroupType { get; }
        public IEnumerable<string> Tags { get; }
        public string Description { get; }
        public bool IsPrivate { get; }
        public CourseStatus CourseStatus { get; }
        public string Curriculum { get; }
    }
}