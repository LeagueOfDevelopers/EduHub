using EduHubLibrary.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService
{
    public class Subscription
    {
        public Subscription(IEventInfo eventInfo)
        {
            Id = Guid.NewGuid();
            EventInfo = eventInfo;
            _subscribers = new List<ISubscriber>();
        }

        public Guid Id { get; private set; }
        /// <summary>
        /// This property describes type of eventInfo that is interesting for subscription
        /// </summary>
        public IEventInfo EventInfo { get; private set; }

        public void AddSubscriber(ISubscriber subscriber)
        {
            _subscribers.Add(subscriber);
        }

        public List<ISubscriber> GetSubscribers()
        {
            return _subscribers;
        }

        public void RemoveSubscriber(ISubscriber subscriber)
        {
            _subscribers.Remove(subscriber);
        }

        public bool IsInterestedInEvent(IEventInfo suggestedEventInfo)
        {
            return EventInfo.Equals(suggestedEventInfo);
        }

        public void NotifySubscriptions(Event @event)
        {
            _subscribers.ForEach(s => s.GetMessage(@event));
        }

        private List<ISubscriber> _subscribers;
    }
}
