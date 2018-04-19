using EduHubLibrary.Domain.Events;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Domain.NotificationService.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Consumers
{
    public class MemberActionsConsumer : IEventConsumer<NewMemberEvent>, IEventConsumer<MemberLeftEvent>
    {
        public MemberActionsConsumer(INotificationsDistributor distributor)
        {
            _distributor = distributor;
        }

        public void Consume(NewMemberEvent @event)
        {
            _distributor.NotifyGroup(@event.GroupId, new NewMemberNotification(@event.GroupTitle, @event.Username));
        }

        public void Consume(MemberLeftEvent @event)
        {
            _distributor.NotifyGroup(@event.GroupId, new MemberLeftNotification(@event.GroupTitle, @event.Username));
        }

        private readonly INotificationsDistributor _distributor;
    }
}
