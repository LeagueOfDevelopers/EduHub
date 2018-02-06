using System;
using EasyNetQ;
using EasyNetQ.Topology;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EduHubLibrary.Domain.Consumers;
using EduHubLibrary.Facades;

namespace EduHubLibrary.Domain.NotificationService
{
    public class EventBus : IEventBus
    {
        public EventBus()
        {
        }

        public void RegisterConsumer<T>(IEventConsumer<T> consumer) where T : EventInfoBase
        {
            //TODO: to add consumer in some collection (create way to keep consumers together)  
        }

        private void ConsumeMessage<T>(T @event) where T : EventInfoBase 
        {
            //TODO: to choose consume
        }

        public void PublishEvent<T>(T @event) where T : EventInfoBase
        {
            ConsumeMessage(@event);
        }
    }
}
