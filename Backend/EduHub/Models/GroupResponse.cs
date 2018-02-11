using System.Collections.Generic;
using EduHub.Models.Tools;

namespace EduHub.Models
{
    public class GroupResponse
    {
        public GroupResponse(FullGroupInfo groupInfo, List<MemberInfo> members)
        {
            GroupInfo = groupInfo;
            Members = members;
        }

        public FullGroupInfo GroupInfo { get; set; }
        public List<MemberInfo> Members { get; set; }
    }
}