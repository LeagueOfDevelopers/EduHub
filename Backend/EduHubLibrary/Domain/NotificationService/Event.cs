using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService
{
    public class Event
    {
        public Event(string eventType)
        {
            Id = Guid.NewGuid();
            EventType = eventType;
            OccurredOn = DateTime.Now;
            IsRead = false;
        }

        public Guid Id { get; private set; }
        public string EventType { get; private set; }
        public DateTime OccurredOn { get; private set; }
        public bool IsRead { get; private set; }

        public void ReadEvent()
        {
            IsRead = true;
        }
    }
}
