using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Events
{
    public class InvitationEvent : EventInfoBase
    {
        public InvitationEvent(Invitation invitation)
        {
            Invitation = invitation;
        }

        public Invitation Invitation { get; private set; }
    }
}
