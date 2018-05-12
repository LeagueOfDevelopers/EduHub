using EduHubLibrary.Domain.Message;
using EduHubLibrary.Domain.NotificationService.Notifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models.ChatControllerModels
{
    public class GroupMessageResponse
    {
        public GroupMessageResponse(int id, DateTimeOffset sentOn, MessageType messageType, INotificationInfo notificationInfo)
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
