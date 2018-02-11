using EduHubLibrary.Domain.NotificationService;

namespace EduHubLibrary.Domain.Events
{
    public class InvitationEvent : IEventInfo
    {
        public InvitationEvent(Invitation invitation)
        {
            Invitation = invitation;
        }

        public Invitation Invitation { get; }

        public EventType GetEventType()
        {
            return EventType.InvitationEvent;
        }
    }
}