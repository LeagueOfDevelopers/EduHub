using EnsureThat;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain
{
    public class Invitation
    {
        public InvitationStatus Status { get; set; }
        public Guid GroupId { get; }
        public Guid FromUser { get; }
        public Guid ToUser { get; }
        public MemberRole SuggestedRole { get; }
        public Guid Id { get; }

        public Invitation(Guid fromUser, Guid toUser, Guid groupId, MemberRole suggestedRole, InvitationStatus status)
        {
            Id = Guid.NewGuid();
            SuggestedRole = suggestedRole;
            Status = Ensure.Any.IsNotNull(status);
            GroupId = Ensure.Guid.IsNotEmpty(groupId);
            FromUser = Ensure.Guid.IsNotEmpty(fromUser);
            ToUser = Ensure.Guid.IsNotEmpty(toUser);
        }
    }
}
