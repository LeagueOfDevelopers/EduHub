using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService
{
    public interface IEventConsumer<T> where T : EventInfoBase
    {
        void Consume(T @event);
    }
}
