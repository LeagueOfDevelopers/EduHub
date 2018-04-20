using EduHubLibrary.Domain.NotificationService.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService
{
    public interface INotificationsDistributor
    {
        void NotifyGroup(int groupId, INotificationInfo notificationInfo);
        void NotifyTeacher(int groupId, INotificationInfo notificationInfo);
        void NotifyPerson(int userId, INotificationInfo notificationInfo);
        void NotifyAdmins(INotificationInfo notificationInfo);
    }
}
