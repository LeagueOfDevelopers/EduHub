using System;

namespace EduHub.Models.NotificationsModels
{
    public class CourseFinishedResponse
    {
        public CourseFinishedResponse(string groupTitle, Guid groupId, string teacherName, Guid teacherId)
        {
            GroupTitle = groupTitle;
            GroupId = groupId;
            TeacherName = teacherName;
            TeacherId = teacherId;
        }

        public string GroupTitle { get; }
        public Guid GroupId { get; }
        public string TeacherName { get; }
        public Guid TeacherId { get; }
    }
}