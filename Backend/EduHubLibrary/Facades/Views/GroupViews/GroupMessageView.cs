using EduHubLibrary.Domain.NotificationService.Notifications;
using EduHubLibrary.Domain.Message;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Facades.Views.GroupViews
{
    public class GroupMessageView : BaseMessageView
    {
        public GroupMessageView(INotificationInfo notificationInfo, int id, DateTimeOffset sentOn)
            : base(id, sentOn)
        {
            NotificationInfo = notificationInfo;
            MessageType = MessageType.GroupMessage;
        }

        public INotificationInfo NotificationInfo { get; }
    }
}
