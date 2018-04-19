using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService.Notifications
{
    public class MemberLeftNotification : INotificationInfo
    {
        public MemberLeftNotification(string groupTitle, string username)
        {
            GroupTitle = groupTitle;
            Username = username;
        }

        public string GroupTitle { get; }
        public string Username { get; }

        public NotificationType GetNotificationType()
        {
            return NotificationType.MemberLeft;
        }
    }
}
