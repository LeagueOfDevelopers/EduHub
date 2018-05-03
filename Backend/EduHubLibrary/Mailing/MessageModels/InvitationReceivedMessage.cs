using EduHubLibrary.Domain;

namespace EduHubLibrary.Mailing.MessageModels
{
    public class InvitationReceivedMessage
    {
        public InvitationReceivedMessage(string groupTitle, string inviterName, MemberRole suggestedRole,
            string receiverName)
        {
            GroupTitle = groupTitle;
            InviterName = inviterName;
            SuggestedRole = suggestedRole;
            ReceiverName = receiverName;
        }

        public string GroupTitle { get; }
        public string InviterName { get; }
        public MemberRole SuggestedRole { get; }
        public string ReceiverName { get; }
    }
}