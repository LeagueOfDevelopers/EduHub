using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService.Notifications
{
    public class CourseFinishedNotification : INotificationInfo
    {
        public CourseFinishedNotification(string groupTitle, string teacherName)
        {
            GroupTitle = groupTitle;
            TeacherName = teacherName;
        }

        public string GroupTitle { get; }
        public string TeacherName { get; }

        public NotificationType GetNotificationType()
        {
            return NotificationType.CourseFinished;
        }
    }
}
