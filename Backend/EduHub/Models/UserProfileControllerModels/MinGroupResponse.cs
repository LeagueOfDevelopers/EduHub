using System.Collections.Generic;

namespace EduHub.Models
{
    public class MinGroupResponse
    {
        public MinGroupResponse(List<MinItemGroupResponse> groups)
        {
            Groups = groups;
        }

        public List<MinItemGroupResponse> Groups { get; set; }
    }
}