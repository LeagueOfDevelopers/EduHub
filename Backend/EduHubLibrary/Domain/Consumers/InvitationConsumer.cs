using EduHubLibrary.Domain.Events;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Facades;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Consumers
{
    public class InvitationConsumer : IEventConsumer<InvitationEvent>
    {
        public InvitationConsumer(IGroupFacade groupFacade)
        {
            _groupFacade = groupFacade;
        }

        public void Consume(InvitationEvent @event)
        {
            _groupFacade.AddInvitation(@event.Invitation.GroupId, @event.Invitation);
        }

        private IGroupFacade _groupFacade;
    }
}
