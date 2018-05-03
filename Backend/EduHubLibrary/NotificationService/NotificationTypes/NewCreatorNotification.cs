namespace EduHubLibrary.Domain.NotificationService.Notifications
{
    public class NewCreatorNotification : INotificationInfo
    {
        public NewCreatorNotification(string groupTitle, string exCreatorUsername, string newCreatorUsername)
        {
            GroupTitle = groupTitle;
            ExCreatorUsername = exCreatorUsername;
            NewCreatorUsername = newCreatorUsername;
        }

        public string GroupTitle { get; }
        public string ExCreatorUsername { get; }
        public string NewCreatorUsername { get; }

        public NotificationType GetNotificationType()
        {
            return NotificationType.NewCreator;
        }
    }
}