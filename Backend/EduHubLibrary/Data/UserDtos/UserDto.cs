using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using EduHubLibrary.Common;
using EduHubLibrary.Data.Connections;
using EduHubLibrary.Data.GroupDtos;
using EduHubLibrary.Data.TagDtos;
using EduHubLibrary.Domain.Tools;

namespace EduHubLibrary.Data.UserDtos
{
    public class UserDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public UserType Type { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Name { get; set; }
        public string AboutUser { get; set; }
        public int BirthYear { get; set; }
        public Gender Gender { get; set; }
        public bool IsTeacher { get; set; }
        public string AvatarLink { get; set; }

        public List<ReviewDto> Reviews { get; set; }
        public List<ContactDto> Contacts { get; set; }
        public List<InvitationDto> Invitations { get; set; }
        public List<NotifiesDto> Notifies { get; set; }

        public ICollection<UserTag> UserTags { get; } = new List<UserTag>();

        [NotMapped]
        public IEnumerable<TagDto> Tags => UserTags.Select(e => e.Tag);

        public UserDto()
        {
        }
    }
}
