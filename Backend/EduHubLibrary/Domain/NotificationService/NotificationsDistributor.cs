using EduHubLibrary.Domain.NotificationService.UserSettings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EduHubLibrary.Common;
using EduHubLibrary.Facades;
using EduHubLibrary.Mailing;
using EduHubLibrary.Domain.Events;
using EduHubLibrary.Domain.NotificationService.Notifications;

namespace EduHubLibrary.Domain.NotificationService
{
    public class NotificationsDistributor : INotificationsDistributor
    {
        public NotificationsDistributor(IGroupRepository groupRepository, IUserRepository userRepository, IEmailSender sender)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _sender = sender;
        }

        public void NotifyAdmins(INotificationInfo notificationInfo)
        {
            _userRepository.GetAll().Where(u => u.Type.Equals(UserType.Admin)).ToList()
                .ForEach(u => NotifySubscriber(u.Id, notificationInfo));
        }

        public void NotifyGroup(int groupId, INotificationInfo notificationInfo)
        {
            _groupRepository.GetGroupById(groupId).Members.ToList().ForEach
                (m => NotifySubscriber(m.UserId, notificationInfo));
        }

        public void NotifyPerson(int userId, INotificationInfo notificationInfo)
        {
            NotifySubscriber(userId, notificationInfo);
        }

        public void NotifyTeacher(int groupId, INotificationInfo notificationInfo)
        {
            var teacherId = _groupRepository.GetGroupById(groupId).Members.Find
                (m => m.MemberRole.Equals(MemberRole.Teacher)).UserId;

            NotifySubscriber(teacherId, notificationInfo);
        }

        private void NotifySubscriber(int userId, INotificationInfo notificationInfo)
        {
            /*
            var user = _userRepository.GetUserById(userId);
            var settings = user.NotificationsSettings.Settings;

            var doesSubscribedOnSite = settings[notificationInfo.GetNotificationType()].Equals(NotificationValue.OnSite) ||
                settings[notificationInfo.GetNotificationType()].Equals(NotificationValue.Everywhere);
            var doesSubscribedOnMail = settings[notificationInfo.GetNotificationType()].Equals(NotificationValue.ToMail) ||
                settings[notificationInfo.GetNotificationType()].Equals(NotificationValue.Everywhere);

            if (doesSubscribedOnSite)
            {
                user.AddNotification(new Notification(notificationInfo));
            }
            
            if (doesSubscribedOnMail)
            {
                var messageContent = MessageMapper.MapNotification(notificationInfo, user.UserProfile.Name);
                //TODO: themes for messages
                _sender.SendMessage(user.UserProfile.Email, messageContent, "");
            }
            */
        }

        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEmailSender _sender;
    }
}
