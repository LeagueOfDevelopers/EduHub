using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Tools;

namespace EduHubLibrary.Data.GroupDtos
{
    public class MemberDto
    {
        public MemberDto(int id, int userId, MemberRole memberRole, bool paid, MemberCurriculumStatus curriculumStatus)
        {
            Id = id;
            UserId = userId;
            MemberRole = memberRole;
            Paid = paid;
            CurriculumStatus = curriculumStatus;
        }

        internal MemberDto()
        {
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public MemberRole MemberRole { get; set; }
        public bool Paid { get; set; }
        public MemberCurriculumStatus CurriculumStatus { get; set; }
    }
}