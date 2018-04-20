using Newtonsoft.Json;
using System;

namespace EduHubLibrary.Domain.NotificationService
{
    public class Event
    {
        public Event(IEventInfo eventInfo)
        {
            Id = Guid.NewGuid();
            OccurredOn = DateTimeOffset.Now;
            EventInfo = JsonConvert.SerializeObject(eventInfo);
            EventType = eventInfo.GetEventType();
        }

        public Guid Id { get; }
        public DateTimeOffset OccurredOn { get; }
        public string EventInfo { get; }
        public EventType EventType { get; }
    }
}