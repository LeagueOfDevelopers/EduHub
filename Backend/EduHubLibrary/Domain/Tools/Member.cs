namespace EduHubLibrary.Domain.Tools
{
    public class Member
    {
        internal Member(int userId, MemberRole memberRole)
        {
            UserId = userId;
            MemberRole = memberRole;
            Paid = false;
            CurriculumStatus = MemberCurriculumStatus.InProgress;
        }

        internal Member(int userId, MemberRole memberRole,
            bool paid, MemberCurriculumStatus curriculumStatus)
        {
            UserId = userId;
            MemberRole = memberRole;
            Paid = paid;
            CurriculumStatus = curriculumStatus;
        }

        public int UserId { get; }
        public MemberRole MemberRole { get; internal set; }
        public bool Paid { get; internal set; }
        public MemberCurriculumStatus CurriculumStatus { get; internal set; }
    }
}