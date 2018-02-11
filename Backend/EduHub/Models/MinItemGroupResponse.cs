using EduHub.Models.Tools;

namespace EduHub.Models
{
    public class MinItemGroupResponse
    {
        public MinItemGroupResponse(MinGroupInfo groupInfo)
        {
            GroupInfo = groupInfo;
        }

        public MinGroupInfo GroupInfo { get; set; }
    }
}