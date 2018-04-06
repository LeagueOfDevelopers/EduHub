using System;
using System.Collections.Generic;
using System.Text;
using EduHubLibrary.Domain;

namespace EduHubLibrary.Facades.Views.GroupViews
{
    public class MinGroupView
    {
        public int Id { get; }
        public string Title { get; }
        public int MemberAmount { get; }
        public int Size { get; }
        public double Cost { get; }
        public GroupType GroupType { get; }
        public IEnumerable<string> Tags { get; }

        public MinGroupView(int id, string title, int memberAmount,
            int size, double cost, GroupType groupType, IEnumerable<string> tags)
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
