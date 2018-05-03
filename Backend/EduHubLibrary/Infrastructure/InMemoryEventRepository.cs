using System.Collections.Generic;
using EduHubLibrary.Domain.NotificationService;
using System;
using EduHubLibrary.Interators;
using EnsureThat;
using EduHubLibrary.Domain.Exceptions;

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
            if (@event == null)
                throw new ArgumentNullException();
            @event.Id = IntIterator.GetNextId();
            _events.Add(@event);
        }

        public Event GetEvent(int eventId)
        {
            return Ensure.Any.IsNotNull(_events.Find(current => current.Id == eventId), nameof(GetEvent),
                opt => opt.WithException(new EventNotFoundException(eventId)));
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return _events;
        }

        public IEnumerable<Event> GetModeratorsHistory()
        {
            var adminsEvents = new List<Event>();
            _events.ForEach(e =>
            {
                if (e.EventType.Equals(EventType.SanctionCancelled) || e.EventType.Equals(EventType.SanctionsApplied))
                    adminsEvents.Add(e);
            });

            return adminsEvents;
        }
    }
}