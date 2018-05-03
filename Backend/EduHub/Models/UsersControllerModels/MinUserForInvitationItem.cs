namespace EduHub.Models
{
    public class MinUserForInvitationItem
    {
        public MinUserForInvitationItem(bool invited, string username, bool isTeacher, int id, string avatarLink)
        {
            Invited = invited;
            Username = username;
            IsTeacher = isTeacher;
            Id = id;
            AvatarLink = avatarLink;
        }

        public bool Invited { get; }
        public string Username { get; }
        public bool IsTeacher { get; }
        public int Id { get; }
        public string AvatarLink { get; }
    }
}