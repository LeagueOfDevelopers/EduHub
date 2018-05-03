using EduHubLibrary.Domain.NotificationService;

namespace EduHubLibrary.EventBus.EventTypes
{
    public class UsingTagEvent : EventInfoBase
    {
        public UsingTagEvent(string tag)
        {
            Tag = tag;
        }

        public string Tag { get; }

        public override EventType GetEventType()
        {
            return EventType.UsingTag;
        }
    }
}