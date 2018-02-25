using EasyNetQ;
using EasyNetQ.Topology;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EduHubLibrary.Domain.NotificationService
{
    public class EventConsumersContainer : IEventConsumersContainer
    {
        public EventConsumersContainer(EventBusSettings eventBusSettings)
        {
            _eventBusSettings = eventBusSettings;
        }

        public void RegisterConsumer<T>(IEventConsumer<T> consumer) where T : EventInfoBase
        {
            var queue = _bus.QueueDeclare(GetQueueNameForConsumer(consumer));
            var routingKey = GetRoutingKeyForEvent<T>();
            _bus.Bind(_mainExchange, queue, routingKey);
            _bus.Consume<T>(queue, (message, info) =>
            Task.Factory.StartNew(() =>
            {
                try
                {
                    consumer.Consume(message.Body);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            ));
        }

        public IEventPublisher GetEventPublisher()
        {
            return new EventPublisher(_bus, _mainExchange);
        }

        public void StartListening()
        {
            InitializeBusConnection();
        }

        public void StopListening()
        {
            _bus.SafeDispose();
        }

        private static string GetQueueNameForConsumer<T>(IEventConsumer<T> consumer)
            where T : EventInfoBase
        {
            return typeof(T).FullName;
        }

        private static string GetRoutingKeyForEvent<T>()
            where T : EventInfoBase
        {
            return typeof(T).FullName;
        }

        private void InitializeBusConnection()
        {
            var connectionString = $"host={_eventBusSettings.HostName}; " +
                                   $"virtualHost={_eventBusSettings.VirtualHost}; " +
                                   $"username={_eventBusSettings.UserName}; " +
                                   $"password={_eventBusSettings.Password}";
            _bus = RabbitHutch.CreateBus("host=localhost").Advanced;
            _mainExchange = _bus.ExchangeDeclare(mainExchangeName, ExchangeType.Direct);
        }
        
        private readonly EventBusSettings _eventBusSettings;
        private IAdvancedBus _bus;
        private IExchange _mainExchange;
        const string mainExchangeName = "all-events";
    }
}
