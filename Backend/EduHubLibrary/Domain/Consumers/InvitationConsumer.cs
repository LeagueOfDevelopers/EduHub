using EduHubLibrary.Domain.Events;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Facades;
using System;

namespace EduHubLibrary.Domain.Consumers
{
    public class InvitationConsumer : IEventConsumer<InvitationEvent>
    {
        private readonly IGroupFacade _groupFacade;

        public InvitationConsumer(IGroupFacade groupFacade)
        {
            _groupFacade = groupFacade;
        }

        public void Consume(InvitationEvent @event)
        {
            _groupFacade.AddInvitation(@event.Invitation.GroupId, @event.Invitation);
        }
    }
}