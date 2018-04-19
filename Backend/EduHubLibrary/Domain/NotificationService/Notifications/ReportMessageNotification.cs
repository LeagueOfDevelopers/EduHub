using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService.Notifications
{
    public class ReportMessageNotification : INotificationInfo
    {
        public ReportMessageNotification(string senderName, string suspectedName, string brokenRule)
        {
            SenderName = senderName;
            SuspectedName = suspectedName;
            BrokenRule = brokenRule;
        }

        public string SenderName { get; }
        public string SuspectedName { get; }
        public string BrokenRule { get; }

        public NotificationType GetNotificationType()
        {
            return NotificationType.ReportMessage;
        }
    }
}
