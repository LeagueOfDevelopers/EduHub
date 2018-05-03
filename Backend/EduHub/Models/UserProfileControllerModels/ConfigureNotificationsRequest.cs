using EduHubLibrary.Domain.NotificationService.Notifications;
using EduHubLibrary.Domain.NotificationService.UserSettings;

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