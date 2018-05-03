namespace EduHubLibrary.Domain.NotificationService.Notifications
{
    public class CurriculumSuggestedNotification : INotificationInfo
    {
        public CurriculumSuggestedNotification(string curriculumLink, string groupTitle)
        {
            CurriculumLink = curriculumLink;
            GroupTitle = groupTitle;
        }

        public string CurriculumLink { get; }
        public string GroupTitle { get; }

        public NotificationType GetNotificationType()
        {
            return NotificationType.CurriculumSuggested;
        }
    }
}