﻿using System.Collections.Generic;

namespace EduHubLibrary.Domain.NotificationService
{
    public interface IEventRepository
    {
        void AddEvent(Event @event);
        IEnumerable<Event> GetAllEvents();
    }
}