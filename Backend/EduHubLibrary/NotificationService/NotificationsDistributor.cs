using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Common;
using EduHubLibrary.Domain.NotificationService.Notifications;
using EduHubLibrary.Domain.NotificationService.UserSettings;
using EduHubLibrary.Mailing;

namespace EduHubLibrary.Domain.NotificationService
{
    public class NotificationsDistributor : INotificationsDistributor
    {
        private readonly IGroupRepository _groupRepository;

        private readonly MessageMapper _messageMapper;
        private readonly Dictionary<NotificationType, string> _messageThemes;
        private readonly IEmailSender _sender;
        private readonly IUserRepository _userRepository;

        public NotificationsDistributor(IGroupRepository groupRepository, IUserRepository userRepository,
            IEmailSender sender)
        {
            _groupRepository = groupRepository;
            _userRepository = userRepository;
            _sender = sender;

            _messageMapper = new MessageMapper();
            _messageThemes = new Dictionary<NotificationType, string>
            {
                {NotificationType.CourseFinished, MessageThemes.CourseFinished},
                {NotificationType.CurriculumAccepted, MessageThemes.CurriculumAccepted},
                {NotificationType.CurriculumDeclined, MessageThemes.CurriculumDeclined},
                {NotificationType.CurriculumSuggested, MessageThemes.CurriculumSuggested},
                {NotificationType.GroupIsFormed, MessageThemes.GroupIsFormed},
                {NotificationType.InvitationAccepted, MessageThemes.InvitationAccepted},
                {NotificationType.InvitationDeclined, MessageThemes.InvitationDeclined},
                {NotificationType.InvitationReceived, MessageThemes.InvitationReceived},
                {NotificationType.MemberLeft, MessageThemes.MemberLeft},
                {NotificationType.NewCreator, MessageThemes.NewCreator},
                {NotificationType.NewMember, MessageThemes.NewMember},
                {NotificationType.ReportMessage, MessageThemes.ReportMessage},
                {NotificationType.ReviewReceived, MessageThemes.ReviewReceived},
                {NotificationType.SanctionsAppliedToAdmin, MessageThemes.SanctionsAppliedForAdmin},
                {NotificationType.SanctionsAppliedToUser, MessageThemes.SanctionsAppliedForUser},
                {NotificationType.SanctionsCancelledToAdmin, MessageThemes.SanctionsCancelledForAdmin},
                {NotificationType.SanctionsCancelledToUser, MessageThemes.SanctionsCancelledForUser},
                {NotificationType.TeacherFound, MessageThemes.TeacherFound}
            };
        }

        public void NotifyAdmins(INotificationInfo notificationInfo)
        {
            _userRepository.GetAll().Where(u => u.Type.Equals(UserType.Admin)).ToList().ForEach(u =>
            {
                NotifyOnMail(u, notificationInfo);
                NotifyOnSite(u, notificationInfo);
            });
        }

        public void NotifyGroup(int groupId, INotificationInfo notificationInfo)
        {
            var currentGroup = _groupRepository.GetGroupById(groupId);
            currentGroup.Members.ToList().ForEach(m => NotifySubscriber(m.UserId, notificationInfo));
            SendNotificationToChat(notificationInfo, currentGroup);
            _groupRepository.Update(currentGroup);
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
            var user = _userRepository.GetUserById(userId);
            var settings = user.NotificationsSettings.Settings;

            var doesSubscribedOnSite =
                settings[notificationInfo.GetNotificationType()].Equals(NotificationValue.OnSite) ||
                settings[notificationInfo.GetNotificationType()].Equals(NotificationValue.Everywhere);
            var doesSubscribedOnMail =
                settings[notificationInfo.GetNotificationType()].Equals(NotificationValue.ToMail) ||
                settings[notificationInfo.GetNotificationType()].Equals(NotificationValue.Everywhere);

            if (doesSubscribedOnSite) NotifyOnSite(user, notificationInfo);

            if (doesSubscribedOnMail) NotifyOnMail(user, notificationInfo);
        }

        private void NotifyOnSite(User user, INotificationInfo notificationInfo)
        {
            user.AddNotification(new Notification(notificationInfo));
            _userRepository.Update(user);
        }

        private void NotifyOnMail(User user, INotificationInfo notificationInfo)
        {
            var messageContent = _messageMapper.MapNotification(notificationInfo, user.UserProfile.Name);
            var notificationType = notificationInfo.GetNotificationType();
            _sender.SendMessage(user.UserProfile.Email, messageContent, _messageThemes[notificationType]);
        }

        private void SendNotificationToChat(INotificationInfo notificationInfo, Group group)
        {
            using (var chat = new ChatSession(group))
            {
                chat.SendGroupMessage(notificationInfo);
            }
        }
    }
}