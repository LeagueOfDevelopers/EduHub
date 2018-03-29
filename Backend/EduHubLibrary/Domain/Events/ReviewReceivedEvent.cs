using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Events
{
    public class ReviewReceivedEvent : EventInfoBase
    {
        public ReviewReceivedEvent(string groupTitle, int groupId, string reviewerName, int reviewId)
        {
            GroupTitle = groupTitle;
            GroupId = groupId;
            ReviewerName = reviewerName;
            ReviewId = reviewId;
        }

        public string GroupTitle { get; }
        public int GroupId { get; }
        public string ReviewerName { get; }
        public int ReviewId { get; }
    }
}
