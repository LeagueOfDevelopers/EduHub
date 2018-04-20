namespace EduHub.Models.NotificationsModels
{
    public class AllPossibleNotifies
    {
        public AllPossibleNotifies(MemberLeftResponse memberLeftResponse, NewMemberResponse newMemberResponse, 
            NewCreatorResponse newCreatorResponse, CourseFinishedResponse courseFinishedResponse, 
            CurriculumAcceptedResponse curriculumAcceptedResponse, CurriculumDeclinedResponse curriculumDeclinedResponse, 
            CurriculumSuggestedResponse curriculumSuggestedResponse, GroupIsFormedResponse groupIsFormedResponse, 
            InvitationAcceptedResponse invitationAcceptedResponse, InvitationDeclinedResponse invitationDeclinedResponse, 
            InvitationReceivedResponse invitationReceivedResponse, ReportMessageResponse reportMessageResponse, 
            ReviewReceivedResponse reviewReceivedResponse, SanctionsAppliedForUserResponse sanctionsAppliedResponse, 
            SanctionsAppliedForAdminResponse sanctionsAppliedForAdminResponse, TeacherFoundResponse teacherFoundResponse)
        {
            MemberLeftResponse = memberLeftResponse;
            NewMemberResponse = newMemberResponse;
            NewCreatorResponse = newCreatorResponse;
            CourseFinishedResponse = courseFinishedResponse;
            CurriculumAcceptedResponse = curriculumAcceptedResponse;
            CurriculumDeclinedResponse = curriculumDeclinedResponse;
            CurriculumSuggestedResponse = curriculumSuggestedResponse;
            GroupIsFormedResponse = groupIsFormedResponse;
            InvitationAcceptedResponse = invitationAcceptedResponse;
            InvitationDeclinedResponse = invitationDeclinedResponse;
            InvitationReceivedResponse = invitationReceivedResponse;
            ReportMessageResponse = reportMessageResponse;
            ReviewReceivedResponse = reviewReceivedResponse;
            SanctionsAppliedResponse = sanctionsAppliedResponse;
            SanctionsAppliedForAdminResponse = sanctionsAppliedForAdminResponse;
            TeacherFoundResponse = teacherFoundResponse;
        }

        public MemberLeftResponse MemberLeftResponse { get; }
        public NewMemberResponse NewMemberResponse { get; }
        public NewCreatorResponse NewCreatorResponse { get; }
        public CourseFinishedResponse CourseFinishedResponse { get; }
        public CurriculumAcceptedResponse CurriculumAcceptedResponse { get; }
        public CurriculumDeclinedResponse CurriculumDeclinedResponse { get; }
        public CurriculumSuggestedResponse CurriculumSuggestedResponse { get; }
        public GroupIsFormedResponse GroupIsFormedResponse { get; }
        public InvitationAcceptedResponse InvitationAcceptedResponse { get; }
        public InvitationDeclinedResponse InvitationDeclinedResponse { get; }
        public InvitationReceivedResponse InvitationReceivedResponse { get; }
        public ReportMessageResponse ReportMessageResponse { get; }
        public ReviewReceivedResponse ReviewReceivedResponse { get; }
        public SanctionsAppliedForUserResponse SanctionsAppliedResponse { get; }
        public SanctionsAppliedForAdminResponse SanctionsAppliedForAdminResponse { get; }
        public TeacherFoundResponse TeacherFoundResponse { get; }
    }
}