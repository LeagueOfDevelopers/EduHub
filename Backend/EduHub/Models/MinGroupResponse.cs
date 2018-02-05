using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models
{
    public class MinGroupResponse
    {
        public List<MinItemGroupResponse> Groups { get; set; }

        public MinGroupResponse(List<MinItemGroupResponse> groups)
        {
            Groups = groups;
        }
    }
}
