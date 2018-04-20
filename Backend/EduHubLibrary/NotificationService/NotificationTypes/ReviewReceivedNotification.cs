using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService.Notifications
{
    public class ReviewReceivedNotification : INotificationInfo
    {
        public ReviewReceivedNotification(string groupTitle, string reviewerName, string reviewType)
        {
            GroupTitle = groupTitle;
            ReviewerName = reviewerName;
            ReviewType = reviewType;
        }

        public string GroupTitle { get; }
        public string ReviewerName { get; }
        public string ReviewType { get; }

        public NotificationType GetNotificationType()
        {
            return NotificationType.ReviewReceived;
        }
    }
}
