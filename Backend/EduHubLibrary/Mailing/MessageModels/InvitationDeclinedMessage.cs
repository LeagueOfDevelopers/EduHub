namespace EduHubLibrary.Mailing.MessageModels
{
    public class InvitationDeclinedMessage
    {
        public InvitationDeclinedMessage(string groupTitle, string invitedName, string receiverName)
        {
            GroupTitle = groupTitle;
            InvitedName = invitedName;
            ReceiverName = receiverName;
        }

        public string GroupTitle { get; }
        public string InvitedName { get; }
        public string ReceiverName { get; }
    }
}