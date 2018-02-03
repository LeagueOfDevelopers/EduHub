using EduHubLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService
{
    public class EventBus : IEventBus
    {
        public EventBus()
        {
            _events = new InMemoryEventRepository();
            _subscriptions = new List<Subscription>();
        }

        /// <summary>
        /// Subscription is connected with type of eventInfo
        /// </summary>
        public Guid AddSubscription(IEventInfo eventInfo)
        {
            _subscriptions.Add(new Subscription(eventInfo));
            return _subscriptions.Last().Id;
        }

        public IEnumerable<ISubscriber> GetSubscribers(Guid subscriptionId)
        {
            return _subscriptions.Find(s => s.Id == subscriptionId).GetSubscribers();
        }

        public void RemoveSubscription(Guid subscriptionId)
        {
            _subscriptions.Remove(_subscriptions.Find(s => s.Id == subscriptionId));
        }

        /// <summary>
        /// Event bus is looking for suitable subscription or creates a new one.
        /// Then bus adds subscriber
        /// </summary>
        public void AddSubscriber(ISubscriber subscriber, IEventInfo eventInfo)
        {
            if (_subscriptions.TrueForAll(s => !s.IsInterestedInEvent(eventInfo)))
            {
                AddSubscription(eventInfo);
                _subscriptions.Last().AddSubscriber(subscriber);
            }
            else
            {
                _subscriptions.Find(s => s.EventInfo.Equals(eventInfo)).AddSubscriber(subscriber);
            }
        }

        public void RemoveSubscriber(ISubscriber subscriber, IEventInfo eventInfo)
        {
            _subscriptions.Find(s => s.EventInfo.Equals(eventInfo)).RemoveSubscriber(subscriber);
        }

        /// <summary>
        /// Event bus checks every subscription if some message is interesting for them.
        /// Interested subscriptions gives command to notify all theit subscriber
        /// </summary>
        public void Notify()
        {
            List<Event> unreadEvents = _events.GetAllEvents().Where(e => e.IsParsed == false).ToList();
            foreach (Event @event in unreadEvents)
            {
                foreach (Subscription subscription in _subscriptions)
                {
                    if (subscription.IsInterestedInEvent(@event.EventInfo))
                    {
                        subscription.NotifySubscriptions(@event);
                    }
                }

                @event.MakeParsed();
            }
        }

        public void SendMessage(Event @event)
        {
            _events.AddMessage(@event);
        }

        public IEnumerable<Subscription> GetSubscriptions()
        {
            return _subscriptions;
        }

        public IEnumerable<Event> GetMessages()
        {
            return _events.GetAllEvents();
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return _events.GetAllEvents();
        }

        private InMemoryEventRepository _events;
        private List<Subscription> _subscriptions;
    }
}
