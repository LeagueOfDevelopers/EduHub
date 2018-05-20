using System;
using EduHubLibrary.Domain.Message;
using EduHubLibrary.Domain.NotificationService.Notifications;

namespace EduHub.Models.ChatControllerModels
{
    public class GroupMessageResponse
    {
        public GroupMessageResponse(int id, DateTimeOffset sentOn, MessageType messageType,
            INotificationInfo notificationInfo)
        {
            Id = id;
            SentOn = sentOn;
            MessageType = messageType;
            NotificationInfo = notificationInfo;
        }


        public INotificationInfo NotificationInfo { get; }
        public int Id { get; }
        public DateTimeOffset SentOn { get; }
        public MessageType MessageType { get; }
    }
}