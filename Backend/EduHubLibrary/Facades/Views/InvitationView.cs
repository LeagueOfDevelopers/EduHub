using EduHubLibrary.Domain;

namespace EduHubLibrary.Facades.Views
{
    public class InvitationView
    {
        public InvitationView(int id, int fromUser, string fromUserName,
            int toUser, string toUserName, int toGroup, string toGroupTitle,
            MemberRole suggestedRole)
        {
            Id = id;
            FromUser = fromUser;
            FromUserName = fromUserName;
            ToUser = toUser;
            ToUserName = toUserName;
            ToGroup = toGroup;
            ToGroupTitle = toGroupTitle;
            SuggestedRole = suggestedRole;
        }

        public int Id { get; set; }
        public int FromUser { get; set; }
        public string FromUserName { get; set; }
        public int ToUser { get; set; }
        public string ToUserName { get; set; }
        public int ToGroup { get; set; }
        public string ToGroupTitle { get; set; }
        public MemberRole SuggestedRole { get; set; }
    }
}
