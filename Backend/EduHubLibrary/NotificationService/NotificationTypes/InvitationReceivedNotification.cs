namespace EduHubLibrary.Domain.NotificationService.Notifications
{
    public class InvitationReceivedNotification : INotificationInfo
    {
        public InvitationReceivedNotification(string groupTitle, string inviterName, MemberRole suggestedRole)
        {
            GroupTitle = groupTitle;
            InviterName = inviterName;
            SuggestedRole = suggestedRole;
        }

        public string GroupTitle { get; }
        public string InviterName { get; }
        public MemberRole SuggestedRole { get; }

        public NotificationType GetNotificationType()
        {
            return NotificationType.InvitationReceived;
        }
    }
}