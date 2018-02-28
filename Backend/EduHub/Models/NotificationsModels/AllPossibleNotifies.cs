namespace EduHub.Models.NotificationsModels
{
    public class AllPossibleNotifies
    {
        public AllPossibleNotifies(MemberLeftResponse memberLeftResponse,
            NewMemberResponse newMemberResponse, NewCreatorResponse newCreatorResponse)
        {
            MemberLeftResponse = memberLeftResponse;
            NewMemberResponse = newMemberResponse;
            NewCreatorResponse = newCreatorResponse;
        }

        public MemberLeftResponse MemberLeftResponse { get; }
        public NewMemberResponse NewMemberResponse { get; }
        public NewCreatorResponse NewCreatorResponse { get; }
    }
}