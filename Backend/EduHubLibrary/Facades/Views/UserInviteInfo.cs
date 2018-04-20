namespace EduHubLibrary.Facades.Views
{
    public class UserInviteInfo
    {
        public UserInviteInfo(bool invited, string username, bool isTeacher,
            int id, string avatarLink, bool isActive)
        {
            Invited = invited;
            Username = username;
            IsTeacher = isTeacher;
            Id = id;
            AvatarLink = avatarLink;
            IsActive = isActive;
        }

        public bool Invited { get; }
        public string Username { get; }
        public bool IsTeacher { get; }
        public int Id { get; }
        public string AvatarLink { get; }
        public bool IsActive { get; }
    }
}