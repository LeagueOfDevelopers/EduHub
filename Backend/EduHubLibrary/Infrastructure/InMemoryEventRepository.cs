using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Infrastructure
{
    public class InMemoryEventRepository : IEventRepository
    {
        public InMemoryEventRepository()
        {
            _events = new List<Event>();
        }

        public void AddMessage(Event @event)
        {
            _events.Add(@event);
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return _events;
        }

        private List<Event> _events;
    }
}
