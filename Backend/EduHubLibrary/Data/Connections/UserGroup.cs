using EduHubLibrary.Data.GroupDtos;
using EduHubLibrary.Data.UserDtos;

namespace EduHubLibrary.Data.Connections
{
    public class UserGroup
    {
        public int UserId { get; set; }
        public UserDto User { get; set; }

        public int GroupId { get; set; }
        public GroupDto Group { get; set; }

        public UserGroup(int userId, UserDto user, int groupId, GroupDto group)
        {
            UserId = userId;
            User = user;
            GroupId = groupId;
            Group = group;
        }

        internal UserGroup()
        {
        }
    }
}
