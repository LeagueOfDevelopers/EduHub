using EduHubLibrary.Domain.NotificationService.Notifications;
using EduHubLibrary.Domain.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using EduHubLibrary.Domain.Message;
using Newtonsoft.Json;

namespace EduHubLibrary.Domain
{
    public class GroupMessage : BaseMessage
    {
        public GroupMessage(INotificationInfo notificationInfo)
        {
            NotificationType = notificationInfo.GetNotificationType();
            NotificationInfo = JsonConvert.SerializeObject(notificationInfo);
        }

        public GroupMessage(INotificationInfo notificationInfo, int id, DateTimeOffset sentOn)
            : base(id, sentOn)
        {
            NotificationType = notificationInfo.GetNotificationType();
            NotificationInfo = JsonConvert.SerializeObject(notificationInfo);
        }

        internal GroupMessage(int id, DateTimeOffset sentOn, string notificationInfo,
            NotificationType notificationType) : base(id, sentOn)
        {
            NotificationInfo = notificationInfo;
            NotificationType = notificationType;
        }

        public string NotificationInfo { get; }
        public NotificationType NotificationType { get; }

        internal override MessageType GetMessageType()
        {
            return MessageType.GroupMessage;
        }
    }
}
