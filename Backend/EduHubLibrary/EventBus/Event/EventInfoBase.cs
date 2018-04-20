namespace EduHubLibrary.Domain.NotificationService
{
    public class EventInfoBase : IEventInfo
    {
        public virtual EventType GetEventType()
        {
            return EventType.Default;
        }
    }
}