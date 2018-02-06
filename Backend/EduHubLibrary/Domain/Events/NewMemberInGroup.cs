using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Events
{
    public class NewMemberInGroup : EventInfoBase
    {
        public NewMemberInGroup(Guid groupId, Guid newMemberId)
        {
            GroupId = groupId;
            NewMemberId = newMemberId;
        }

        public Guid GroupId { get; private set; }
        public Guid NewMemberId { get; private set; }
    }
}
