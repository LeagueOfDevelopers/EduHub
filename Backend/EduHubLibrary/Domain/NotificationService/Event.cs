using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService
{
    public class Event
    {
        public Event(string eventType)
        {
            Id = new Guid();
            EventType = eventType;
            OccurredOn = DateTime.Now;
        }

        public Guid Id { get; private set; }
        public string EventType { get; private set; }
        public DateTime OccurredOn { get; private set; }
    }
}
