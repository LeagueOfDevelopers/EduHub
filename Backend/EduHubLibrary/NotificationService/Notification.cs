using EduHubLibrary.Domain.NotificationService.Notifications;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace EduHubLibrary.Domain.NotificationService
{
    public class Notification
    {
        public Notification(INotificationInfo notificationInfo)
        {
            OccurredOn = DateTimeOffset.Now;
            NotificationInfo = JsonConvert.SerializeObject(notificationInfo);
            NotificationType = notificationInfo.GetNotificationType();
        }

        public DateTimeOffset OccurredOn { get; }
        public string NotificationInfo { get; }
        public NotificationType NotificationType { get; }
    }
}
