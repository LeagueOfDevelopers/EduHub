using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Events
{
    public class NewMemberEvent : EventInfoBase
    {
        public NewMemberEvent(Guid groupId, Guid newMemberId)
        {
            GroupId = groupId;
            NewMemberId = newMemberId;
        }

        public Guid GroupId { get; }
        public Guid NewMemberId { get; }
    }
}
