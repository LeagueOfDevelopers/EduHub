using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService
{
    public interface IEventPublisher
    {
        void PublishEvent<T>(T @event) where T : EventInfoBase;
    }
}
