using EduHubLibrary.Domain.NotificationService.UserSettings;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using EduHubLibrary.Common;
using EduHubLibrary.Facades;

namespace EduHubLibrary.Domain.NotificationService
{
    public class NotificationsDistributor : INotificationsDistributor
    {
        public NotificationsDistributor(IGroupRepository groupRepository, IUserRepository userRepository)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
        }

        public void NotifyAdmins(IEventInfo eventInfo)
        {
            _userRepository.GetAll().Where(u => u.Type.Equals(UserType.Admin)).ToList().ForEach(u => u.AddNotify(""));
        }

        public void NotifyGroup(int groupId, IEventInfo eventInfo)
        {
            _groupRepository.GetGroupById(groupId).Members.ToList().ForEach
                (m => _userRepository.GetUserById(m.UserId).AddNotify(""));
        }

        public void NotifyPerson(int userId, IEventInfo eventInfo)
        {
            _userRepository.GetUserById(userId).AddNotify("");
        }
        
        private readonly IGroupRepository _groupRepository;
        private readonly IUserRepository _userRepository;
    }
}
