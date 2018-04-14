using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Mailing.EmailModels
{
    public class CourseFinishedMessage
    {
        public CourseFinishedMessage(string groupTitle, string teacherName)
        {
            GroupTitle = groupTitle;
            TeacherName = teacherName;
        }

        public string GroupTitle { get; }
        public string TeacherName { get; }
    }
}
