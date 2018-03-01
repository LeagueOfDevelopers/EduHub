using System;

namespace EduHub.Models.NotificationsModels
{
    public class TeacherFoundResponse
    {
        public string TeacherName { get; }
        public Guid TeacherId { get; }
        public string GroupTitle { get; }
        public Guid GroupId { get; }

        public TeacherFoundResponse(string teacherName, Guid teacherId, string groupTitle, Guid groupId)
        {
            TeacherName = teacherName;
            TeacherId = teacherId;
            GroupTitle = groupTitle;
            GroupId = groupId;
        }
    }
}
