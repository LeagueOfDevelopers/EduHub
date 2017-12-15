using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain
{
    internal class Member
    {
        internal Member(Guid userId, MemberRole memberRole)
        {
            UserId = userId;
            MemberRole = memberRole;
        }

        internal void ChangeRole(MemberRole newRole)
        {
            MemberRole = newRole;
        }

        public Guid UserId{ get; protected set; }
        public MemberRole MemberRole { get; protected set; }
    }
}
