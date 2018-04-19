using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService.Notifications
{
    public class InvitationDeclinedNotification : INotificationInfo
    {
        public InvitationDeclinedNotification(string groupTitle, string invitedName)
        {
            GroupTitle = groupTitle;
            InvitedName = invitedName;
        }

        public string GroupTitle { get; }
        public string InvitedName { get; }

        public NotificationType GetNotificationType()
        {
            return NotificationType.InvitationDeclined;
        }
    }
}
