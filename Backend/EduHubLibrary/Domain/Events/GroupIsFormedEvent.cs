using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Events
{
    public class GroupIsFormedEvent : EventInfoBase
    {
        public GroupIsFormedEvent(string groupTitle, int groupId)
        {
            GroupTitle = groupTitle;
            GroupId = groupId;
        }

        public string GroupTitle { get; }
        public int GroupId { get; }
    }
}
