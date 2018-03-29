using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Events
{
    public class CourseFinishedEvent : EventInfoBase
    {
        public CourseFinishedEvent(string groupTitle, int groupId, string teacherName, int teacherId)
        {
            GroupTitle = groupTitle;
            GroupId = groupId;
            TeacherName = teacherName;
            TeacherId = teacherId;
        }

        public string GroupTitle { get; }
        public int GroupId { get; }
        public string TeacherName { get; }
        public int TeacherId { get; }
    }
}
