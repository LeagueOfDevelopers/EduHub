using System;
using EduHubLibrary.Domain;

namespace EduHubLibrary.Data.UserDtos
{
    public class InvitationDto
    {
        public int Id { get; set; }
        public InvitationStatus Status { get; set; }
        public int GroupId { get; set; }
        public int FromUser { get; set; }
        public int ToUser { get; set; }
        public MemberRole SuggestedRole { get; set; }

        public InvitationDto(int id, InvitationStatus status,
            int groupId, int fromUser, int toUser, MemberRole suggestedRole)
        {
            Id = id;
            Status = status;
            GroupId = groupId;
            FromUser = fromUser;
            ToUser = toUser;
            SuggestedRole = suggestedRole;
        }
    }
}
