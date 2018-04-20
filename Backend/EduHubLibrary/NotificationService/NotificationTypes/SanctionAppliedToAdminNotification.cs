using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService.Notifications
{
    public class SanctionAppliedToAdminNotification : INotificationInfo
    {
        public SanctionAppliedToAdminNotification(string brokenRule, SanctionType sanctionType, string username)
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
            return NotificationType.SanctionsAppliedToAdmin;
        }
    }
}
