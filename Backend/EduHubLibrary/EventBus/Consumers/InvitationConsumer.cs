using EduHubLibrary.Domain.Events;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Domain.NotificationService.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Consumers
{
    public class InvitationConsumer : IEventConsumer<InvitationAcceptedEvent>, IEventConsumer<InvitationDeclinedEvent>,
        IEventConsumer<InvitationReceivedEvent>
    {
        public InvitationConsumer(INotificationsDistributor distributor)
        {
            _distributor = distributor;
        }

        public void Consume(InvitationAcceptedEvent @event)
        {
            _distributor.NotifyPerson(@event.SenderId, new InvitationAcceptedNotification(@event.GroupTitle, @event.InvitedName));
        }

        public void Consume(InvitationDeclinedEvent @event)
        {
            _distributor.NotifyPerson(@event.SenderId, new InvitationDeclinedNotification(@event.GroupTitle, @event.InvitedName));
        }

        public void Consume(InvitationReceivedEvent @event)
        {
            _distributor.NotifyPerson(@event.InvitedId, new InvitationReceivedNotification(@event.GroupTitle, @event.InviterName, @event.SuggestedRole));
        }

        private readonly INotificationsDistributor _distributor;
    }
}
