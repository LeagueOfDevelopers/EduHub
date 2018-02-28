using EduHubLibrary.Domain.NotificationService;

namespace EduHubLibrary.Domain.Events
{
    public class InvitationEvent : EventInfoBase
    {
        public InvitationEvent(Invitation invitation)
        {
            Invitation = invitation;
        }

        public Invitation Invitation { get; }

        public override EventType GetEventType()
        {
            return EventType.InvitationEvent;
        }
    }
}