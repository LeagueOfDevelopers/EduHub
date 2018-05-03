using EduHubLibrary.Domain.NotificationService;

namespace EduHubLibrary.EventBus.EventTypes
{
    public class InvitationDeclinedEvent : EventInfoBase
    {
        public InvitationDeclinedEvent(string groupTitle, string invitedName, int senderId)
        {
            GroupTitle = groupTitle;
            InvitedName = invitedName;
            SenderId = senderId;
        }

        public string GroupTitle { get; }
        public string InvitedName { get; }
        public int SenderId { get; }

        public override EventType GetEventType()
        {
            return EventType.InvitationDeclined;
        }
    }
}