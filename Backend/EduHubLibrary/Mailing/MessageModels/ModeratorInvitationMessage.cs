namespace EduHubLibrary.Mailing.MessageModels
{
    public class ModeratorInvitationMessage
    {
        public ModeratorInvitationMessage(int inviteCode)
        {
            InviteCode = inviteCode;
        }

        public int InviteCode { get; }
    }
}