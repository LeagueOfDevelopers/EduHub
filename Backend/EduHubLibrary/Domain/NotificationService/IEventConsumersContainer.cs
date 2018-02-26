﻿namespace EduHubLibrary.Domain.NotificationService
{
    public interface IEventConsumersContainer
    {
        void RegisterConsumer<T>(IEventConsumer<T> consumer) where T : EventInfoBase;
        void StartListening();
        void StopListening();
    }
}