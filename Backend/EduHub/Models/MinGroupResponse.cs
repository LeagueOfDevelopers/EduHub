using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models
{
    public class MinGroupResponse
    {
        public IEnumerable<MinItemGroupResponse> Groups { get; set; }

        public MinGroupResponse(IEnumerable<MinItemGroupResponse> groups)
        {
            Groups = groups;
        }
    }
}
