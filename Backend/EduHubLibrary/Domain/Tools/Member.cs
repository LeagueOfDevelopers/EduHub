using System;

namespace EduHubLibrary.Domain.Tools
{
    public class Member
    {
        internal Member(Guid userId, MemberRole memberRole)
        {
            UserId = userId;
            MemberRole = memberRole;
            Paid = false;
            CurriculumStatus = MemberCurriculumStatus.InProgress;
        }

        public Guid UserId { get; }
        public MemberRole MemberRole { get; internal set; }
        public bool Paid { get; internal set; }
        public MemberCurriculumStatus CurriculumStatus { get; internal set; }
    }
}