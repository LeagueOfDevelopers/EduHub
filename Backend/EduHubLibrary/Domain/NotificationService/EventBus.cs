using System;
using System.Threading.Tasks;
using EasyNetQ;
using EasyNetQ.Topology;

namespace EduHubLibrary.Domain.NotificationService
{
    public class EventBus : IEventBus
    {
        private const string mainExchangeName = "all-events";
        private readonly EventBusSettings _eventBusSettings;
        private IAdvancedBus _bus;
        private IExchange _mainExchange;

        public EventBus(EventBusSettings eventBusSettings)
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

        public void StartListening()
        {
            InitializeBusConnection();
        }

        public void StopListening()
        {
            _bus.SafeDispose();
        }

        public IEventPublisher GetEventPublisher()
        {
            return new EventPublisher(_bus, _mainExchange);
        }

        private string GetQueueNameForConsumer<T>(IEventConsumer<T> consumer) where T : EventInfoBase
        {
            return typeof(T).FullName;
        }

        private string GetRoutingKeyForEvent<T>() where T : EventInfoBase
        {
            return typeof(T).FullName;
        }

        private void InitializeBusConnection()
        {
            var connectionString = $"host={_eventBusSettings.HostName}; " +
                                   $"virtualHost={_eventBusSettings.VirtualHost}; " +
                                   $"username={_eventBusSettings.UserName}; " +
                                   $"password={_eventBusSettings.Password}";
            _bus = RabbitHutch.CreateBus(connectionString).Advanced;
            _mainExchange = _bus.ExchangeDeclare(mainExchangeName, ExchangeType.Direct);
        }
    }
}