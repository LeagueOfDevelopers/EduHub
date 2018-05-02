using Newtonsoft.Json;
using System;

namespace EduHubLibrary.Domain.NotificationService
{
    public class Event
    {
        public Event(IEventInfo eventInfo, int id = 0)
        {
            Id = id;
            OccurredOn = DateTimeOffset.Now;
            EventInfo = JsonConvert.SerializeObject(eventInfo);
            EventType = eventInfo.GetEventType();
        }

        public int Id { get; internal set; }
        public DateTimeOffset OccurredOn { get; }
        public string EventInfo { get; }
        public EventType EventType { get; }
    }
}