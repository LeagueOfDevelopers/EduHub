using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Events
{
    public class InvitationToGroupEvent : IEventInfo
    {
        public InvitationToGroupEvent(Invitation invitation)
        {
            Invitation = invitation;
        }

        public Invitation Invitation { get; private set; }
    
        public string GetDescription()
        {
            return ($"В группу приглашен пользователь с id {Invitation.ToUser} на роль {Invitation.SuggestedRole}");
        }

        public string GetEventType()
        {
            return GetType().Name;
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType().Equals(GetType()))
            {
                InvitationToGroupEvent newObj = (InvitationToGroupEvent)obj;

                if (Invitation.GroupId.Equals(newObj.Invitation.GroupId)) return true;
                else return false;
            }
            else return false;           
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
