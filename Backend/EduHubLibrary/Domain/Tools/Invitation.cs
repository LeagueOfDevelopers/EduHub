using EnsureThat;

namespace EduHubLibrary.Domain
{
    public class Invitation
    {
        public Invitation(int fromUser, int toUser, int groupId, MemberRole suggestedRole, InvitationStatus status,
            int id = 0)
        {
            Id = id;
            SuggestedRole = suggestedRole;
            Status = Ensure.Any.IsNotNull(status);
            GroupId = groupId;
            FromUser = fromUser;
            ToUser = toUser;
        }

        public InvitationStatus Status { get; set; }
        public int GroupId { get; }
        public int FromUser { get; }
        public int ToUser { get; }
        public MemberRole SuggestedRole { get; }
        public int Id { get; internal set; }
    }
}