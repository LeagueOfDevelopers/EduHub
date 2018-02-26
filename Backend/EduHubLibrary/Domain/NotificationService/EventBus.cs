namespace EduHubLibrary.Domain.NotificationService
{
    /*
    public class EventBus : IEventBus
    {
        private readonly Dictionary<EventType, object> _consumers;
        private readonly IEventRepository _events;

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

            var eventConsumer = (IEventConsumer<T>) _consumers[eventType];
            eventConsumer.Consume(@event);
        }
    }
    */
}