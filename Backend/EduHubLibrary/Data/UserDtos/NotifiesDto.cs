using System;
using EduHubLibrary.Domain.NotificationService.Notifications;

namespace EduHubLibrary.Data.UserDtos
{
    public class NotifiesDto
    {
        public NotifiesDto(int id, DateTimeOffset occurredOn, string notificationInfo,
            NotificationType notificationType)
        {
            Id = id;
            OccurredOn = occurredOn;
            NotificationInfo = notificationInfo;
            NotificationType = notificationType;
        }

        public NotifiesDto()
        {
        }

        public int Id { get; set; }
        public DateTimeOffset OccurredOn { get; set; }
        public string NotificationInfo { get; set; }
        public NotificationType NotificationType { get; set; }
    }
}