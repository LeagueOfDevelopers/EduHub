using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService
{
    public interface IEventConsumer<T> where T : IEventInfo
    {
        void Consume(T @event);
    }
}
