using System;
using System.Collections.Generic;
using System.Text;

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
