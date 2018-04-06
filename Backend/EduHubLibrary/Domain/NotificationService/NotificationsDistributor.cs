using EduHubLibrary.Domain.NotificationService.UserSettings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EduHubLibrary.Common;
using EduHubLibrary.Facades;
using EduHubLibrary.Mailing;

namespace EduHubLibrary.Domain.NotificationService
{
    public class NotificationsDistributor : INotificationsDistributor
    {
        public NotificationsDistributor(IGroupRepository groupRepository, IUserRepository userRepository, EmailSender sender)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _sender = sender;
        }

        public void NotifyAdmins(IEventInfo eventInfo)
        {
            _userRepository.GetAll().Where(u => u.Type.Equals(UserType.Admin)).ToList()
                .ForEach(u => NotifySubscriber(u.Id, eventInfo));
        }

        public void NotifyGroup(int groupId, IEventInfo eventInfo)
        {
            _groupRepository.GetGroupById(groupId).Members.ToList().ForEach
                (m => NotifySubscriber(m.UserId, eventInfo));
        }

        public void NotifyPerson(int userId, IEventInfo eventInfo)
        {
            NotifySubscriber(userId, eventInfo);
        }

        public void NotifyTeacher(int groupId, IEventInfo eventInfo)
        {
            var teacherId = _groupRepository.GetGroupById(groupId).Members.Find
                (m => m.MemberRole.Equals(MemberRole.Teacher)).UserId;

            NotifySubscriber(teacherId, eventInfo);
        }

        private void NotifySubscriber(int userId, IEventInfo eventInfo)
        {
            var user = _userRepository.GetUserById(userId);
            var settings = user.NotificationsSettings.Settings;

            var doesSubscribedOnSite = settings[eventInfo.GetEventType()].Equals(NotificationValue.OnSite) ||
                settings[eventInfo.GetEventType()].Equals(NotificationValue.Everywhere);
            var doesSubscribedOnMail = settings[eventInfo.GetEventType()].Equals(NotificationValue.ToMail) ||
                settings[eventInfo.GetEventType()].Equals(NotificationValue.Everywhere);

            if (doesSubscribedOnSite)
            {
                user.AddNotification(new Event(eventInfo));
            }

            if (doesSubscribedOnMail)
            {
                //emails' sending will be here
            }
        }

        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly EmailSender _sender;
    }
}
