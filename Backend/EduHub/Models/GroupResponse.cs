using EduHub.Models.Tools;
using System.Collections.Generic;

namespace EduHub.Models
{
    public class GroupResponse
    {
        public FullGroupInfo GroupInfo { get; set; }
        public List<MemberInfo> Members { get; set; }

        public GroupResponse(FullGroupInfo groupInfo, List<MemberInfo> members)
        {
            GroupInfo = groupInfo;
            Members = members;
        }
    }
}
