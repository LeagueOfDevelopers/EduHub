using System;

namespace EduHubLibrary.Domain.NotificationService
{
    public class Event
    {
        public Event(IEventInfo eventInfo)
        {
            Id = Guid.NewGuid();
            OccurredOn = DateTimeOffset.Now;
            EventInfo = eventInfo;
        }

        public Guid Id { get; }
        public DateTimeOffset OccurredOn { get; }
        public IEventInfo EventInfo { get; }
    }
}