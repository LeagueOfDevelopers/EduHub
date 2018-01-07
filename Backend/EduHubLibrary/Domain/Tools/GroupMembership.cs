using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Tools
{
    public class GroupMembership
    {
        public GroupMembership(Group group, MemberRole memberRole)
        {
            Group = group;
            MemberRole = memberRole;
        }

        Group Group { get; set; }
        MemberRole MemberRole { get; set; }
    }
}
