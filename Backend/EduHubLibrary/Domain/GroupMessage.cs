using EduHubLibrary.Domain.NotificationService.Notifications;
using EduHubLibrary.Domain.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain
{
    public class GroupMessage : BaseMessage
    {
        public GroupMessage(INotificationInfo notificationInfo)
        {
            Notification = notificationInfo;
        }

        public GroupMessage(INotificationInfo notificationInfo, int id, DateTimeOffset sentOn)
            : base(id, sentOn)
        {
            Notification = notificationInfo;
        }

        public INotificationInfo Notification { get; }
    }
}
