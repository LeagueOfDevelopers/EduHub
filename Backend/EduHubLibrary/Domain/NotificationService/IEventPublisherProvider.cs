﻿using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.NotificationService
{
    public interface IEventPublisherProvider
    {
        IEventPublisher GetEventPublisher();
    }
}