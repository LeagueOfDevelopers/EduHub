using System.Collections.Generic;
using EduHub.Models.Tools;

namespace EduHub.Models
{
    public class GroupResponse
    {
        public GroupResponse(FullGroupInfo groupInfo, IEnumerable<MemberInfo> members,
            IEnumerable<ReviewModel> reviews)
        {
            GroupInfo = groupInfo;
            Members = members;
            Reviews = reviews;
        }

        public FullGroupInfo GroupInfo { get; set; }
        public IEnumerable<MemberInfo> Members { get; set; }
        public IEnumerable<ReviewModel> Reviews { get; }
    }
}