using System;
using System.Collections.Generic;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Tools;

namespace EduHubLibrary.Facades.Views.GroupViews
{
    public class GroupInfoView
    {
        public GroupInfoView(int groupId, string title, int size,
            int memberAmount, double price, GroupType groupType, IEnumerable<string> tags,
            string description, string curriculum, bool isPrivate, bool isActive,
            CourseStatus courseStatus, int votersAmount)
        {
            GroupId = groupId;
            Title = title;
            Size = size;
            MemberAmount = memberAmount;
            Price = price;
            GroupType = groupType;
            Tags = tags;
            Description = description;
            Curriculum = curriculum;
            IsPrivate = isPrivate;
            IsActive = isActive;
            CourseStatus = courseStatus;
            VotersAmount = votersAmount;
        }

        public int GroupId { get; }
        public string Title { get; }
        public int Size { get; }
        public int MemberAmount { get; }
        public double Price { get; }
        public GroupType GroupType { get; }
        public IEnumerable<string> Tags { get; }
        public string Description { get; }
        public string Curriculum { get; }
        public bool IsPrivate { get; }
        public bool IsActive { get; }
        public CourseStatus CourseStatus { get; }
        public int VotersAmount { get; }
    }
}