using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService
{
    public interface IPublisher
    {
        void AddSubscriber(ISubscriber subscriber);
        void NotifySubscribers();
        void RemoveSubscriber(ISubscriber subscriber);
    }
}
