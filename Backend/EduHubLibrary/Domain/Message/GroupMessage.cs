using EduHubLibrary.Domain.NotificationService.Notifications;
using EduHubLibrary.Domain.Tools;
using System;
using System.Collections.Generic;
using System.Text;
using EduHubLibrary.Domain.Message;

namespace EduHubLibrary.Domain
{
    public class GroupMessage : BaseMessage
    {
        public GroupMessage(INotificationInfo notificationInfo)
        {
            NotificationInfo = notificationInfo;
        }

        public GroupMessage(INotificationInfo notificationInfo, int id, DateTimeOffset sentOn)
            : base(id, sentOn)
        {
            NotificationInfo = notificationInfo;
        }

        public INotificationInfo NotificationInfo { get; }

        internal override MessageType GetMessageType()
        {
            return MessageType.GroupMessage;
        }
    }
}
