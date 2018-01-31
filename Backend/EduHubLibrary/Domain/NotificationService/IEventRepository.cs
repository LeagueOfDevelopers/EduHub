using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService
{
    public interface IEventRepository
    {
        void AddEvent(Event @event);
        List<Event> GetAllEvents();
    }
}
