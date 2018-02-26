using EasyNetQ;
using EasyNetQ.Topology;

namespace EduHubLibrary.Domain.NotificationService
{
    public class EventPublisher : IEventPublisher
    {
        private readonly IAdvancedBus _bus;
        private readonly IExchange _exchange;

        public EventPublisher(IAdvancedBus bus, IExchange exchange)
        {
            _bus = bus;
            _exchange = exchange;
        }

        public void PublishEvent<T>(T @event) where T : EventInfoBase
        {
            var routingKey = typeof(T).FullName;
            var message = new Message<T>(@event);
            _bus.Publish(_exchange, routingKey, false, message);
        }
    }
}