using EduHubLibrary.Domain.NotificationService.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService
{
    public interface INotificationsDistributor
    {
        void NotifyGroup(int groupId, INotificationInfo eventInfo);
        void NotifyTeacher(int groupId, INotificationInfo eventInfo);
        void NotifyPerson(int userId, INotificationInfo eventInfo);
        void NotifyAdmins(INotificationInfo eventInfo);
    }
}
