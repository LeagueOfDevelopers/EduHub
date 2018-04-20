using System;

namespace EduHub.Models.NotificationsModels
{
    public class ReviewReceivedResponse
    {
        public ReviewReceivedResponse(string groupTitle, string reviewerName, string reviewType)
        {
            GroupTitle = groupTitle;
            ReviewerName = reviewerName;
            ReviewType = reviewType;
        }

        public string GroupTitle { get; }
        public string ReviewerName { get; }
        public string ReviewType { get; }
    }
}