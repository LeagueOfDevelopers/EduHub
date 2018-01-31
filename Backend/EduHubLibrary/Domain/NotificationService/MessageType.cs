using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService
{
    public enum MessageType
    {
        Default = 0,
        ToGroupMembers = 1,
        ToStudents = 2,
        ToTeacher = 3,
        ToUser = 4
    }
}
