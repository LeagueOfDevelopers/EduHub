using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.EventBus.EventTypes
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

        public override EventType GetEventType()
        {
            return EventType.GroupIsFormed;
        }
    }
}
