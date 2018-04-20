using System;

namespace EduHub.Models.NotificationsModels
{
    public class CourseFinishedResponse
    {
        public CourseFinishedResponse(string groupTitle, string teacherName)
        {
            GroupTitle = groupTitle;
            TeacherName = teacherName;
        }

        public string GroupTitle { get; }
        public string TeacherName { get; }
    }
}