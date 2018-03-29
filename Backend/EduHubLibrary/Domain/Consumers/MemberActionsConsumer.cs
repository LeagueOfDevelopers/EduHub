using EduHubLibrary.Domain.Events;
using EduHubLibrary.Domain.NotificationService;
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
            _distributor.NotifyGroup(@event.GroupId, @event);
        }

        public void Consume(MemberLeftEvent @event)
        {
            _distributor.NotifyGroup(@event.GroupId, @event);
        }

        private readonly INotificationsDistributor _distributor;
    }
}
