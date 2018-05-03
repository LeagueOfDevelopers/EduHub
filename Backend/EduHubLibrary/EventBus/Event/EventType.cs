namespace EduHubLibrary.Domain.NotificationService
{
    public enum EventType
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
        NewMember = 11,
        ReportMessage = 12,
        ReviewReceived = 13,
        SanctionsApplied = 14,
        TeacherFound = 15,
        UsingTag = 16,
        SanctionCancelled = 17
    }
}