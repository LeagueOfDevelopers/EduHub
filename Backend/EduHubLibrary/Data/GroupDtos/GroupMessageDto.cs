using EduHubLibrary.Domain.NotificationService.Notifications;
using System;

namespace EduHubLibrary.Data.GroupDtos
{
    public class GroupMessageDto
    {
        public GroupMessageDto(int id, DateTimeOffset sentOn, NotificationType notificationType,
            string notificationInfo)
        {
            Id = id;
            SentOn = sentOn;
            NotificationType = notificationType;
            NotificationInfo = notificationInfo;
        }

        public GroupMessageDto()
        {
        }

        public int Id { get; internal set; }
        public DateTimeOffset SentOn { get; }
        public NotificationType NotificationType { get; set; }
        public string NotificationInfo { get; set; }

    }
}
