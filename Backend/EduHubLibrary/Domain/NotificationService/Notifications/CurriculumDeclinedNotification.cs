using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService.Notifications
{
    public class CurriculumDeclinedNotification : INotificationInfo
    {
        public CurriculumDeclinedNotification(string groupTitle, string declinedName)
        {
            GroupTitle = groupTitle;
            DeclinedName = declinedName;
        }

        public string GroupTitle { get; }
        public string DeclinedName { get; }

        public NotificationType GetNotificationType()
        {
            return NotificationType.CurriculumDeclined;
        }
    }
}
