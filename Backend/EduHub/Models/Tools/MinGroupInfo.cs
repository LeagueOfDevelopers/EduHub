using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;

namespace EduHub.Models.Tools
{
    public class MinGroupInfo
    {
        /// <summary>
        /// group id
        /// </summary>
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int MemberAmount{ get; set; }
        public int Size { get; set; }
        public double Cost { get; set; }
        public GroupType GroupType { get; set; }
        public IEnumerable<string> Tags { get; set; }

        public MinGroupInfo(Guid id, string title, int memberAmount, int size, double cost, GroupType groupType, IEnumerable<string> tags)
        {
            Id = id;
            Title = title;
            MemberAmount = memberAmount;
            Size = size;
            Cost = cost;
            GroupType = groupType;
            Tags = tags;
        }
    }
}
