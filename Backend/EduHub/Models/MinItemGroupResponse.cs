using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models
{
    public class MinItemGroupResponse
    {
        public GroupInfo GroupInfo { get; set; }


        public MinItemGroupResponse(GroupInfo groupInfo)
        {
            GroupInfo = groupInfo;
        }
    }
}
