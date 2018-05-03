using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Domain.NotificationService.Notifications;

namespace EduHubLibrary.Domain.NotificationService.UserSettings
{
    public class NotificationsSettings
    {
        public Dictionary<NotificationType, NotificationValue> Settings;

        public NotificationsSettings()
        {
            Settings = new Dictionary<NotificationType, NotificationValue>
            {
                {NotificationType.CourseFinished, NotificationValue.Everywhere},
                {NotificationType.CurriculumAccepted, NotificationValue.Everywhere},
                {NotificationType.CurriculumDeclined, NotificationValue.Everywhere},
                {NotificationType.CurriculumSuggested, NotificationValue.Everywhere},
                {NotificationType.GroupIsFormed, NotificationValue.Everywhere},
                {NotificationType.InvitationAccepted, NotificationValue.Everywhere},
                {NotificationType.InvitationDeclined, NotificationValue.Everywhere},
                {NotificationType.InvitationReceived, NotificationValue.Everywhere},
                {NotificationType.MemberLeft, NotificationValue.Everywhere},
                {NotificationType.NewCreator, NotificationValue.Everywhere},
                {NotificationType.NewMember, NotificationValue.Everywhere},
                {NotificationType.ReviewReceived, NotificationValue.Everywhere},
                {NotificationType.SanctionsAppliedToUser, NotificationValue.Everywhere},
                {NotificationType.SanctionsCancelledToUser, NotificationValue.Everywhere},
                {NotificationType.TeacherFound, NotificationValue.Everywhere}
            };
        }

        internal void ConfigureSettings(NotificationType configuringNotification, NotificationValue newValue)
        {
            var newSettings =
                new Dictionary<NotificationType, NotificationValue>(Settings.ToDictionary(kv => kv.Key, kv => kv.Value))
                {
                    [configuringNotification] = newValue
                };
            Settings = newSettings;
        }
    }
}