using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService
{
    public interface IEventBus
    {
        IEnumerable<ISubscriber> GetSubscribers(IEventInfo eventInfo);
        void AddSubscriber(ISubscriber subscriber, IEventInfo eventInfo);
        void RemoveSubscriber(ISubscriber subscriber, IEventInfo eventInfo);
        void Notify();
        void SendMessage(Event @event);
        IEnumerable<Event> GetAllEvents();
    }
}
