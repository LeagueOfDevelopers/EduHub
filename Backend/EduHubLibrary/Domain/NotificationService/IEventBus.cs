using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService
{
    public interface IEventBus
    {
        Guid AddSubscription(IEventInfo eventInfo);
        IEnumerable<ISubscriber> GetSubscribers(Guid subscriptionId);
        void RemoveSubscription(Guid subscriptionId);
        void AddSubscriber(ISubscriber subscriber, IEventInfo eventInfo);
        void RemoveSubscriber(ISubscriber subscriber, IEventInfo eventInfo);
        void Notify();
        void SendMessage(Event @event);
        IEnumerable<Event> GetAllEvents();
    }
}
