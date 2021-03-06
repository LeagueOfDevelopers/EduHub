﻿using System;
using EduHubLibrary.Domain.Message;
using EduHubLibrary.Domain.NotificationService.Notifications;
using EduHubLibrary.Domain.Tools;
using Newtonsoft.Json;

namespace EduHubLibrary.Domain
{
    public class GroupMessage : BaseMessage
    {
        public GroupMessage(INotificationInfo notificationInfo)
        {
            SentOn = DateTimeOffset.UtcNow;
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