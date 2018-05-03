namespace EduHubLibrary.Mailing.MessageModels
{
    public class AdminInvitationMessage
    {
        public AdminInvitationMessage(int inviteCode)
        {
            InviteCode = inviteCode;
        }

        public int InviteCode { get; }
    }
}