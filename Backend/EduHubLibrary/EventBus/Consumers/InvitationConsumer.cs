using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Domain.NotificationService.Notifications;
using EduHubLibrary.EventBus.EventTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Consumers
{
    public class InvitationConsumer : IEventConsumer<InvitationAcceptedEvent>, IEventConsumer<InvitationDeclinedEvent>,
        IEventConsumer<InvitationReceivedEvent>
    {
        public InvitationConsumer(INotificationsDistributor distributor, IEventRepository eventRepository)
        {
            _distributor = distributor;
            _eventRepository = eventRepository;
        }

        public void Consume(InvitationAcceptedEvent @event)
        {
            _distributor.NotifyPerson(@event.SenderId, new InvitationAcceptedNotification(@event.GroupTitle, @event.InvitedName));
            _eventRepository.AddEvent(new Event(@event));
        }

        public void Consume(InvitationDeclinedEvent @event)
        {
            _distributor.NotifyPerson(@event.SenderId, new InvitationDeclinedNotification(@event.GroupTitle, @event.InvitedName));
            _eventRepository.AddEvent(new Event(@event));
        }

        public void Consume(InvitationReceivedEvent @event)
        {
            _distributor.NotifyPerson(@event.InvitedId, new InvitationReceivedNotification(@event.GroupTitle, @event.InviterName, @event.SuggestedRole));
            _eventRepository.AddEvent(new Event(@event));
        }

        private readonly INotificationsDistributor _distributor;
        private readonly IEventRepository _eventRepository;
    }
}
