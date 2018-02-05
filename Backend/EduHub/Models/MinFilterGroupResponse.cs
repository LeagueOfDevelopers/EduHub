using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models
{
    public class MinFilterGroupResponse
    {
        public IEnumerable<MinItemGroupResponse> FillingGroups { get; set; }
        public IEnumerable<MinItemGroupResponse> FullGroups { get; set; }

        public MinFilterGroupResponse(IEnumerable<MinItemGroupResponse> fullGroups, IEnumerable<MinItemGroupResponse> fillingGroups)
        {
            FullGroups = fullGroups;
            FillingGroups = fillingGroups;
        }
    }
}
