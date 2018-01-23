using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain
{
    public class Member
    {
        internal Member(Guid userId, MemberRole memberRole)
        {
            UserId = userId;
            MemberRole = memberRole;
            Paid = false;
            AcceptedCurriculum = false;
        }

        public Guid UserId { get; private set; }
        public MemberRole MemberRole { get; set; }
        public bool Paid { get; set; }
        public bool AcceptedCurriculum { get; set; }
    }
}
