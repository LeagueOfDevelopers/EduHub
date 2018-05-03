namespace EduHubLibrary.Domain.NotificationService.Notifications
{
    public class CurriculumAcceptedNotification : INotificationInfo
    {
        public CurriculumAcceptedNotification(string groupTitle)
        {
            GroupTitle = groupTitle;
        }

        public string GroupTitle { get; }

        public NotificationType GetNotificationType()
        {
            return NotificationType.CurriculumAccepted;
        }
    }
}