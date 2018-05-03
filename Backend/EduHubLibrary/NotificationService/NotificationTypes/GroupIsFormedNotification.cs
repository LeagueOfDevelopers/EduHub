namespace EduHubLibrary.Domain.NotificationService.Notifications
{
    public class GroupIsFormedNotification : INotificationInfo
    {
        public GroupIsFormedNotification(string groupTitle)
        {
            GroupTitle = groupTitle;
        }

        public string GroupTitle { get; }

        public NotificationType GetNotificationType()
        {
            return NotificationType.GroupIsFormed;
        }
    }
}