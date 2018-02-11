﻿using System.Collections.Generic;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Tools;

namespace EduHub.Models.Tools
{
    public class FullGroupInfo
    {
        public FullGroupInfo(string title, int size, int currentAmount, double cost, GroupType groupType,
            IEnumerable<string> tags, string description, CourseStatus courseStatus)
        {
            Title = title;
            Size = size;
            CurrentAmount = currentAmount;
            Cost = cost;
            GroupType = groupType;
            Tags = tags;
            Description = description;
            CourseStatus = courseStatus;
        }

        public string Title { get; set; }
        public int Size { get; set; }
        public int CurrentAmount { get; set; }
        public double Cost { get; set; }
        public GroupType GroupType { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public string Description { get; set; }
        public CourseStatus CourseStatus { get; set; }
    }
}