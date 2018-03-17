using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using EduHubLibrary.Data.Connections;
using EduHubLibrary.Data.TagDtos;
using EduHubLibrary.Data.UserDtos;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Tools;

namespace EduHubLibrary.Data.GroupDtos
{
    public class GroupDto
    {
        internal GroupDto()
        {
        }

        public int Id { get;  set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Curriculum { get; set; }
        public bool IsPrivate { get; set; }
        public bool IsActive { get; set; }
        public int Size { get; set; }
        public double Price { get; set; }
        public CourseStatus Status { get; set; }
        public GroupType GroupType { get; set; }

        public int TeacherId { get; set; }
        public string TeacherName { get; set; }
        public string TeacherEmail { get; set; }

        public List<MessageDto> Messages { get; set; } = new List<MessageDto>();
        public List<MemberDto> Members { get; set; } = new List<MemberDto>();
        public List<InvitationDto> Invitations { get; set; } = new List<InvitationDto>();

        public ICollection<GroupTag> GroupTags { get; } = new List<GroupTag>();

        [NotMapped]
        public IEnumerable<TagDto> Tags => GroupTags.Select(e => e.Tag);
    }
}
