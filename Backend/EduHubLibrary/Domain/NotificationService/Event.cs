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
            IsParsed = false;
            EventInfo = eventInfo;
            Description = eventInfo.GetDescription();
        }

        public Guid Id { get; private set; }
        public DateTime OccurredOn { get; private set; }
        public bool IsParsed { get; private set; }
        public IEventInfo EventInfo { get; private set; }
        public string Description { get; private set; }

        /// <summary>
        /// Change label when event is read with event bus
        /// </summary>
        public void MakeParsed()
        {
            IsParsed = true;
        }
    }
}
