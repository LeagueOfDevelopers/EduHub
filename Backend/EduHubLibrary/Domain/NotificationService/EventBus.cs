using System;
using EasyNetQ;
using EasyNetQ.Topology;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EduHubLibrary.Domain.Consumers;
using EduHubLibrary.Facades;
using System.Collections;
using EduHubLibrary.Domain.Events;
using EduHubLibrary.Infrastructure;

namespace EduHubLibrary.Domain.NotificationService
{
    public class EventBus : IEventBus
    {
        public EventBus()
        {
            _events = new InMemoryEventRepository();
            _consumers = new Dictionary<EventType, object>();
        }
        
        public void RegisterConsumer<T>(IEventConsumer<T> consumer, EventType eventType) where T : IEventInfo
        {
            _consumers.Add(eventType, consumer);
        }

        public void PublishEvent<T>(T @event) where T : IEventInfo
        {
            _events.AddEvent(new Event(@event));
            ConsumeEvent(@event);
        }

        private void ConsumeEvent<T>(T @event) where T : IEventInfo
        {
            var eventType = @event.GetEventType();

            IEventConsumer<T> eventConsumer = (IEventConsumer<T>)_consumers[eventType];
            eventConsumer.Consume(@event);
        }

        private Dictionary<EventType, object> _consumers;
        private readonly IEventRepository _events;
    }
}