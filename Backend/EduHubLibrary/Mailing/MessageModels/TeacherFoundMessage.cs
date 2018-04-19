using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Mailing.MessageModels
{
    public class TeacherFoundMessage
    {
        public TeacherFoundMessage(string teacherName, string groupTitle, string receiverName)
        {
            TeacherName = teacherName;
            GroupTitle = groupTitle;
            ReceiverName = receiverName;
        }

        public string TeacherName { get; }
        public string GroupTitle { get; }
        public string ReceiverName { get; }
    }
}
