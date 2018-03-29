using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService.UserSettings
{
    public enum NotificationType
    {
        Default = 0,
        CourseFinished = 1,
        CurriculumAccepted = 2,
        CurriculumDeclined = 3,
        CurriculumSuggested = 4,
        GroupIsFormed = 5,
        InvitationAccepted = 6,
        InvitationDeclined = 7,
        InvitationReceived = 8,
        MemberLeft = 9,
        NewCreator = 10,
        ReportMessage = 11,
        ReviewReceived = 12,
        SanctiosApplied = 14,
        TeacherFound = 15
    }
}
