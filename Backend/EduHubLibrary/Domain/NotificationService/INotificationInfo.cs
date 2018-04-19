using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService.Notifications
{
    public interface INotificationInfo
    {
        NotificationType GetNotificationType();
    }
}
