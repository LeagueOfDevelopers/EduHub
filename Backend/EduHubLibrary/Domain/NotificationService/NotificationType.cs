using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService.Notifications
{
    public enum NotificationType
    {
        Default,
        CourseFinished,
        CurriculumAccepted,
        CurriculumDeclined,
        CurriculumSuggested,
        GroupIsFormed,
        InvitationAccepted,
        InvitationDeclined,
        InvitationReceived,
        MemberLeft,
        NewCreator,
        NewMember,
        ReportMessage,
        ReviewReceived,
        SanctionsAppliedToUser,
        SanctionsAppliedToAdmin,
        TeacherFound
    }
}
