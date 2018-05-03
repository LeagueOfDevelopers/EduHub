using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Domain.NotificationService.Notifications;
using EduHubLibrary.EventBus.EventTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Consumers
{
    public class GroupEventsConsumer : IEventConsumer<NewCreatorEvent>, IEventConsumer<GroupIsFormedEvent>
    {
        public GroupEventsConsumer(INotificationsDistributor distributor, IEventRepository eventRepository)
        {
            _distributor = distributor;
            _eventRepository = eventRepository;
        }

        public void Consume(NewCreatorEvent @event)
        {
            _distributor.NotifyGroup(@event.GroupId, new NewCreatorNotification(@event.GroupTitle, @event.ExCreatorUsername,
                @event.NewCreatorUsername));
            _eventRepository.AddEvent(new Event(@event));
        }

        public void Consume(GroupIsFormedEvent @event)
        {
            _distributor.NotifyGroup(@event.GroupId, new GroupIsFormedNotification(@event.GroupTitle));
            _eventRepository.AddEvent(new Event(@event));
        }

        private readonly INotificationsDistributor _distributor;
        private readonly IEventRepository _eventRepository;
    }
}
