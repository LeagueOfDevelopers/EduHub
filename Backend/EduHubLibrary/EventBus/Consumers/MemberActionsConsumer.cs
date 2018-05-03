using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Domain.NotificationService.Notifications;
using EduHubLibrary.EventBus.EventTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Consumers
{
    public class MemberActionsConsumer : IEventConsumer<NewMemberEvent>, IEventConsumer<MemberLeftEvent>
    {
        public MemberActionsConsumer(INotificationsDistributor distributor, IEventRepository eventRepository)
        {
            _distributor = distributor;
            _eventRepository = eventRepository;
        }

        public void Consume(NewMemberEvent @event)
        {
            _distributor.NotifyGroup(@event.GroupId, new NewMemberNotification(@event.GroupTitle, @event.Username));
            _eventRepository.AddEvent(new Event(@event));
        }

        public void Consume(MemberLeftEvent @event)
        {
            _distributor.NotifyGroup(@event.GroupId, new MemberLeftNotification(@event.GroupTitle, @event.Username));
            _eventRepository.AddEvent(new Event(@event));
        }

        private readonly INotificationsDistributor _distributor;
        private readonly IEventRepository _eventRepository;
    }
}
