using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService
{
    public interface IEventInfo
    {
        string GetEventType();
        string GetDescription();
    }
}
