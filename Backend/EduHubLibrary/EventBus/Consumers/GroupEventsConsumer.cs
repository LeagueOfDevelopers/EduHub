using EduHubLibrary.Domain.Events;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Domain.NotificationService.Notifications;
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
            _distributor.NotifyGroup(@event.GroupId, new NewCreatorNotification(@event.GroupTitle, @event.ExCreatorUsername,
                @event.NewCreatorUsername));
        }

        public void Consume(GroupIsFormedEvent @event)
        {
            _distributor.NotifyGroup(@event.GroupId, new GroupIsFormedNotification(@event.GroupTitle));
        }

        private readonly INotificationsDistributor _distributor;
    }
}
