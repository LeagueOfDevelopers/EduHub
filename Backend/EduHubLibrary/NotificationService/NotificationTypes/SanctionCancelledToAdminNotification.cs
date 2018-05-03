using EduHubLibrary.Domain;
using EduHubLibrary.Domain.NotificationService.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.NotificationService.NotificationTypes
{
    public class SanctionCancelledToAdminNotification : INotificationInfo
    {
        public SanctionCancelledToAdminNotification(string brokenRule, SanctionType sanctionType, string username)
        {
            BrokenRule = brokenRule;
            SanctionType = sanctionType;
            Username = username;
        }

        public string BrokenRule { get; }
        public SanctionType SanctionType { get; }
        public string Username { get; }

        public NotificationType GetNotificationType()
        {
            return NotificationType.SanctionsCancelledToAdmin;
        }
    }
}
