using System;

namespace EduHub.Models.NotificationsModels
{
    public class ReviewReceivedResponse
    {
        public ReviewReceivedResponse(string groupTitle, Guid groupId, string reviewerName, Guid reviewId)
        {
            GroupTitle = groupTitle;
            GroupId = groupId;
            ReviewerName = reviewerName;
            ReviewId = reviewId;
        }

        public string GroupTitle { get; }
        public Guid GroupId { get; }
        public string ReviewerName { get; }
        public Guid ReviewId { get; }
    }
}