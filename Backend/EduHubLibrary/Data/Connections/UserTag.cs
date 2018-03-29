using EduHubLibrary.Data.TagDtos;
using EduHubLibrary.Data.UserDtos;

namespace EduHubLibrary.Data.Connections
{
    public class UserTag
    {
        public UserTag(string tagId, TagDto tag, int userId, UserDto user)
        {
            TagId = tagId;
            Tag = tag;
            UserId = userId;
            User = user;
        }

        internal UserTag()
        {
        }

        public string TagId { get; set; }
        public TagDto Tag { get; set; }

        public int UserId { get; set; }
        public UserDto User { get; set; }
    }
}