using System.Collections.Generic;
using EduHubLibrary.Domain.NotificationService;

namespace EduHubLibrary.Infrastructure
{
    public class InMemoryEventRepository : IEventRepository
    {
        private readonly List<Event> _events;

        public InMemoryEventRepository()
        {
            _events = new List<Event>();
        }

        public void AddEvent(Event @event)
        {
            _events.Add(@event);
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return _events;
        }
    }
}