namespace EduHub.Models.NotificationsModels
{
    public class AllPossibleNotifies
    {
        public MemberLeftResponse MemberLeftResponse { get; }
        public NewMemberResponse NewMemberResponse { get; }
        public NewCreatorResponse NewCreatorResponse { get; }

        public AllPossibleNotifies(MemberLeftResponse memberLeftResponse,
            NewMemberResponse newMemberResponse, NewCreatorResponse newCreatorResponse)
        {
            MemberLeftResponse = memberLeftResponse;
            NewMemberResponse = newMemberResponse;
            NewCreatorResponse = newCreatorResponse;
        }
    }
}
