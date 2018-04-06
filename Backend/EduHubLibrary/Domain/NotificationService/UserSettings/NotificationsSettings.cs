using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService.UserSettings
{
    public class NotificationsSettings
    {
        public NotificationsSettings()
        {
            Settings = new Dictionary<EventType, NotificationValue>
            {
                { EventType.CourseFinished, NotificationValue.Everywhere },
                { EventType.CurriculumAccepted, NotificationValue.Everywhere },
                { EventType.CurriculumDeclined, NotificationValue.Everywhere },
                { EventType.CurriculumSuggested, NotificationValue.Everywhere },
                { EventType.GroupIsFormed, NotificationValue.Everywhere },
                { EventType.InvitationAccepted, NotificationValue.Everywhere },
                { EventType.InvitationDeclined, NotificationValue.Everywhere },
                { EventType.InvitationReceived, NotificationValue.Everywhere },
                { EventType.MemberLeft, NotificationValue.Everywhere },
                { EventType.NewCreator, NotificationValue.Everywhere },
                { EventType.NewMember, NotificationValue.Everywhere },
                { EventType.ReviewReceived, NotificationValue.Everywhere },
                { EventType.SanctionsApplied, NotificationValue.Everywhere },
                { EventType.TeacherFound, NotificationValue.Everywhere }
            };
        }

        internal void ConfigureSettings(EventType configuringEvent, NotificationValue newValue)
        {
            var newSettings = new Dictionary<EventType, NotificationValue>(Settings.ToDictionary(kv => kv.Key, kv => kv.Value))
            {
                [configuringEvent] = newValue
            };
            Settings = newSettings;
        }

        public IReadOnlyDictionary<EventType, NotificationValue> Settings;
    }
}
