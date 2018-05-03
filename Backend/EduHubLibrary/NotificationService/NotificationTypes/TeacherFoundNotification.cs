namespace EduHubLibrary.Domain.NotificationService.Notifications
{
    public class TeacherFoundNotification : INotificationInfo
    {
        public TeacherFoundNotification(string teacherName, string groupTitle)
        {
            TeacherName = teacherName;
            GroupTitle = groupTitle;
        }

        public string TeacherName { get; }
        public string GroupTitle { get; }

        public NotificationType GetNotificationType()
        {
            return NotificationType.TeacherFound;
        }
    }
}