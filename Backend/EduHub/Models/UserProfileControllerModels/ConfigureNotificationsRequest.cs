using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Domain.NotificationService.UserSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models.UserProfileControllerModels
{
    public class ConfigureNotificationsRequest
    {
        public ConfigureNotificationsRequest(EventType configuringEvent, NotificationValue newValue)
        {
            ConfiguringEvent = configuringEvent;
            NewValue = newValue;
        }

        public EventType ConfiguringEvent { get; set; }
        public NotificationValue NewValue { get; set; }
    }
}
