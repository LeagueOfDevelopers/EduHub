namespace EduHubLibrary.Domain.NotificationService
{
    public interface IEventBus
    {
        void RegisterConsumer<T>(IEventConsumer<T> consumer) where T : EventInfoBase;
        void StartListening();
        void StopListening();
    }
}