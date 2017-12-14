using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain
{
    class Member
    {
        public Member(Guid userId, MemberRole memberRole)
        {
            UserId = userId;
            MemberRole = memberRole;
        }

        public Guid UserId{ get; protected set; }
        public MemberRole MemberRole { get; protected set; }
    }
}
