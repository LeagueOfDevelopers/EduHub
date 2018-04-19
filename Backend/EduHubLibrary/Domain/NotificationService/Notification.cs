using EduHubLibrary.Domain.NotificationService.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService
{
    public class Notification
    {
        public Notification(INotificationInfo notificationInfo)
        {
            OccurredOn = DateTimeOffset.Now;
            NotificationInfo = notificationInfo;
            NotificationType = notificationInfo.GetNotificationType();
        }

        public DateTimeOffset OccurredOn { get; }
        public INotificationInfo NotificationInfo { get; }
        public NotificationType NotificationType { get; }
    }
}
