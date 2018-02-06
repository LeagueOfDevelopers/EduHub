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

        public Guid Id { get; private set; }
        public DateTime OccurredOn { get; private set; }
        public IEventInfo EventInfo { get; private set; }
    }
}
