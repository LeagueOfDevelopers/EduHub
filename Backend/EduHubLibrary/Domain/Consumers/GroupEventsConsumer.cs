﻿using EduHubLibrary.Domain.Events;
using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Consumers
{
    public class GroupEventsConsumer : IEventConsumer<NewCreatorEvent>, IEventConsumer<GroupIsFormedEvent>
    {
        public GroupEventsConsumer(INotificationsDistributor distributor)
        {
            _distributor = distributor;
        }

        public void Consume(NewCreatorEvent @event)
        {
            _distributor.NotifyGroup(@event.GroupId, @event);
        }

        public void Consume(GroupIsFormedEvent @event)
        {
            _distributor.NotifyGroup(@event.GroupId, @event);
        }

        private readonly INotificationsDistributor _distributor;
    }
}
