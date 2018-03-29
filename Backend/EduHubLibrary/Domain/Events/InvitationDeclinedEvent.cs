using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Events
{
    public class InvitationDeclinedEvent : EventInfoBase
    {
        public InvitationDeclinedEvent(string groupTitle, int groupId, string invitedName, int invitedId, int senderId)
        {
            GroupTitle = groupTitle;
            GroupId = groupId;
            InvitedName = invitedName;
            InvitedId = invitedId;
            SenderId = senderId;
        }

        public string GroupTitle { get; }
        public int GroupId { get; }
        public string InvitedName { get; }
        public int InvitedId { get; }
        public int SenderId { get; }
    }
}
