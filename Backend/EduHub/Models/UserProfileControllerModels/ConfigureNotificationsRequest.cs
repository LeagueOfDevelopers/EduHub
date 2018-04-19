using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Domain.NotificationService.Notifications;
using EduHubLibrary.Domain.NotificationService.UserSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models.UserProfileControllerModels
{
    public class ConfigureNotificationsRequest
    {
        public ConfigureNotificationsRequest(NotificationType configuringNotification, NotificationValue newValue)
        {
            ConfiguringNotification = configuringNotification;
            NewValue = newValue;
        }

        public NotificationType ConfiguringNotification { get; set; }
        public NotificationValue NewValue { get; set; }
    }
}
