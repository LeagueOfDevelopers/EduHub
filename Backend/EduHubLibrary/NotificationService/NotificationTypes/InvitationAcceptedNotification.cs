namespace EduHubLibrary.Domain.NotificationService.Notifications
{
    public class InvitationAcceptedNotification : INotificationInfo
    {
        public InvitationAcceptedNotification(string groupTitle, string invitedName)
        {
            GroupTitle = groupTitle;
            InvitedName = invitedName;
        }

        public string GroupTitle { get; }
        public string InvitedName { get; }

        public NotificationType GetNotificationType()
        {
            return NotificationType.InvitationAccepted;
        }
    }
}