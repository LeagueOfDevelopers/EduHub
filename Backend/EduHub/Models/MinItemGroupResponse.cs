using EduHub.Models.Tools;
using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models
{
    public class MinItemGroupResponse
    {
        public MinGroupInfo GroupInfo { get; set; }

        public MinItemGroupResponse(MinGroupInfo groupInfo)
        {
            GroupInfo = groupInfo;
        }
    }
}
