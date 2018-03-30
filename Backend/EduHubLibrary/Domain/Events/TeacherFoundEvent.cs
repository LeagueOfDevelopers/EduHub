using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Events
{
    public class TeacherFoundEvent : EventInfoBase
    {
        public TeacherFoundEvent(string teacherName, string groupTitle, int groupId)
        {
            TeacherName = teacherName;
            GroupTitle = groupTitle;
            GroupId = groupId;
        }

        public string TeacherName { get; }
        public string GroupTitle { get; }
        public int GroupId { get; }
    }
}
