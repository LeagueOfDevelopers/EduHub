using System;
using EasyNetQ;
using EasyNetQ.Topology;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EduHubLibrary.Domain.Consumers;
using EduHubLibrary.Facades;
using System.Collections;

namespace EduHubLibrary.Domain.NotificationService
{
    public class EventBus : IEventBus
    {
        public EventBus()
        {
            _consumers = new Dictionary<string, dynamic>();
        }

        public void RegisterConsumer<T>(IEventConsumer<T> consumer) where T : EventInfoBase
        {
            _consumers.Add(typeof(T).FullName, consumer);
        }
        
        public void PublishEvent<T>(T @event) where T : EventInfoBase
        {
            ConsumeMessage(@event);
        }

        private void ConsumeMessage<T>(T @event) where T : EventInfoBase
        {
            _consumers[typeof(T).FullName].Consume(@event);
        }

        private Dictionary<string, dynamic> _consumers;
    }
}