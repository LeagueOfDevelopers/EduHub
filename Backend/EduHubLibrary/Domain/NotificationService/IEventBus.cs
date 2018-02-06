using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService
{
    public interface IEventBus
    {
        void RegisterConsumer<T>(IEventConsumer<T> consumer) where T : EventInfoBase;
        void PublishEvent<T>(T @event) where T : EventInfoBase;
    }
}
