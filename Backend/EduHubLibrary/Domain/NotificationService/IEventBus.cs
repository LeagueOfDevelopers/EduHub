namespace EduHubLibrary.Domain.NotificationService
{
    public interface IEventBus
    {
        void RegisterConsumer<T>(IEventConsumer<T> consumer, EventType eventType) where T : IEventInfo;
        void PublishEvent<T>(T @event) where T : IEventInfo;
    }
}