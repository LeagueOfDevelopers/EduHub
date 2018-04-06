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
                .ForEach(u => u.AddNotify(new Event(eventInfo)));
        }

        public void NotifyGroup(int groupId, IEventInfo eventInfo)
        {
            _groupRepository.GetGroupById(groupId).Members.ToList().ForEach
                (m => _userRepository.GetUserById(m.UserId).AddNotify(new Event(eventInfo)));
        }

        public void NotifyPerson(int userId, IEventInfo eventInfo)
        {
            _userRepository.GetUserById(userId).AddNotify(new Event(eventInfo));
        }

        public void NotifyTeacher(int groupId, IEventInfo eventInfo)
        {
            var teacherId = _groupRepository.GetGroupById(groupId).Members.Find
                (m => m.MemberRole.Equals(MemberRole.Teacher)).UserId;

            _userRepository.GetUserById(teacherId).AddNotify(new Event(eventInfo));
        }

        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
        private readonly EmailSender _sender;
    }
}
