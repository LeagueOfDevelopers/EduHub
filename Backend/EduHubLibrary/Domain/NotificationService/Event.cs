using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService
{
    public class Event
    {
        public Event(IEventInfo eventInfo)
        {
            Id = Guid.NewGuid();
            OccurredOn = DateTime.Now;
            EventInfo = eventInfo;
        }

        public Guid Id { get; }
        public DateTime OccurredOn { get; }
        public IEventInfo EventInfo { get; }
    }
}
