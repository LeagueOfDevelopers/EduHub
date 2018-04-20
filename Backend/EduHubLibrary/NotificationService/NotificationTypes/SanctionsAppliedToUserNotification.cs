using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService.Notifications
{
    public class SanctionsAppliedToUserNotification : INotificationInfo
    {
        public SanctionsAppliedToUserNotification(string brokenRule, SanctionType sanctionType)
        {
            BrokenRule = brokenRule;
            SanctionType = sanctionType;
        }

        public string BrokenRule { get; }
        public SanctionType SanctionType { get; }

        public NotificationType GetNotificationType()
        {
            return NotificationType.SanctionsAppliedToUser;
        }
    }
}
