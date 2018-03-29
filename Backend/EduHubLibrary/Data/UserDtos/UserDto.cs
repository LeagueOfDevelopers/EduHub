using System.Collections.Generic;
using EduHubLibrary.Common;
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
        public List<TagUser> Tags { get; set; }
    }
}