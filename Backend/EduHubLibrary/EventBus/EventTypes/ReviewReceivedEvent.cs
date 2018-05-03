using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.EventBus.EventTypes
{
    public class ReviewReceivedEvent : EventInfoBase
    {
        public ReviewReceivedEvent(string groupTitle, int groupId, string reviewerName, string reviewType)
        {
            GroupTitle = groupTitle;
            GroupId = groupId;
            ReviewerName = reviewerName;
            ReviewType = reviewType;
        }

        public string GroupTitle { get; }
        public int GroupId { get; }
        public string ReviewerName { get; }
        public string ReviewType { get; }

        public override EventType GetEventType()
        {
            return EventType.ReviewReceived;
        }
    }
}
