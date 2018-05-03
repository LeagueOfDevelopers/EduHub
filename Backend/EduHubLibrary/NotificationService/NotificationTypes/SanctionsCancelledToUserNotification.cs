using EduHubLibrary.Domain;
using EduHubLibrary.Domain.NotificationService.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.NotificationService.NotificationTypes
{
    public class SanctionsCancelledToUserNotification : INotificationInfo
    {
        public SanctionsCancelledToUserNotification(string brokenRule, SanctionType sanctionType)
        {
            BrokenRule = brokenRule;
            SanctionType = sanctionType;
        }

        public string BrokenRule { get; }
        public SanctionType SanctionType { get; }

        public NotificationType GetNotificationType()
        {
            return NotificationType.SanctionsCancelledToUser;
        }
    }
}
