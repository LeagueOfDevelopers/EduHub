namespace EduHubLibrary.Domain.NotificationService.Notifications
{
    public class NewMemberNotification : INotificationInfo
    {
        public NewMemberNotification(string groupTitle, string username)
        {
            GroupTitle = groupTitle;
            Username = username;
        }

        public string GroupTitle { get; }
        public string Username { get; }

        public NotificationType GetNotificationType()
        {
            return NotificationType.NewMember;
        }
    }
}