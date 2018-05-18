using EduHubLibrary.Domain.NotificationService.Notifications;
using EduHubLibrary.Domain.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Facades.Views.GroupViews
{
    public class GroupMessageView : BaseMessageView
    {
        public GroupMessageView(string notificationInfo, NotificationType notificationType, int id, DateTimeOffset sentOn)
            : base(id, sentOn)
        {
            NotificationInfo = notificationInfo;
            MessageType = MessageType.GroupMessage;
            NotificationType = NotificationType;
        }

        public string NotificationInfo { get; }
        public NotificationType NotificationType { get; }
    }
}
