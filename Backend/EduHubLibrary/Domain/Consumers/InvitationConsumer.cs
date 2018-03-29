using EduHubLibrary.Domain.Events;
using EduHubLibrary.Domain.NotificationService;
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
            _distributor.NotifyPerson(@event.SenderId, @event);
        }

        public void Consume(InvitationDeclinedEvent @event)
        {
            _distributor.NotifyPerson(@event.SenderId, @event);
        }

        public void Consume(InvitationReceivedEvent @event)
        {
            _distributor.NotifyPerson(@event.InvitedId, @event);
        }

        private readonly INotificationsDistributor _distributor;
    }
}
