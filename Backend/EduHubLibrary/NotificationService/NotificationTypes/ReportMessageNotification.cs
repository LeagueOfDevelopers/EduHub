namespace EduHubLibrary.Domain.NotificationService.Notifications
{
    public class ReportMessageNotification : INotificationInfo
    {
        public ReportMessageNotification(string senderName, string suspectedName, string reason, string description)
        {
            SenderName = senderName;
            SuspectedName = suspectedName;
            Reason = reason;
            Description = description;
        }

        public string SenderName { get; }
        public string SuspectedName { get; }
        public string Reason { get; }
        public string Description { get; }

        public NotificationType GetNotificationType()
        {
            return NotificationType.ReportMessage;
        }
    }
}