namespace EduHub.Models.NotificationsModels
{
    public class InvitationDeclinedResponse
    {
        public InvitationDeclinedResponse(string groupTitle, string invitedName)
        {
            GroupTitle = groupTitle;
            InvitedName = invitedName;
        }

        public string GroupTitle { get; }
        public string InvitedName { get; }
    }
}