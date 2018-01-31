using EduHubLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService
{
    public class MessageBus : IMessageBus
    {
        public MessageBus()
        {
            _events = new InMemoryEventRepository();
            _subscribers = new List<ISubscriber>();
        }

        public void AddSubscriber(ISubscriber subscriber)
        {
            _subscribers.Add(subscriber);
        }

        public void Notify()
        {
            foreach (ISubscriber subscriber in _subscribers)
            {
                List<Event> unreadEvents = _events.GetAllEvents().Where(e => e.IsRead.Equals(false)).ToList();
                foreach (Event @event in unreadEvents)
                {
                    subscriber.GetMessage(@event);
                }
                _events.GetAllEvents().ForEach(e => e.ReadEvent());
            }
        }

        public void RemoveSubscriber(ISubscriber subscriber)
        {
            _subscribers.Remove(subscriber);
        }

        public void SendMessage(Event @event)
        {
            _events.AddEvent(@event);
        }

        private InMemoryEventRepository _events;
        private List<ISubscriber> _subscribers;
    }
}
