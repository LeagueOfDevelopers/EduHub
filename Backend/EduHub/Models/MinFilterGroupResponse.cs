using System.Collections.Generic;

namespace EduHub.Models
{
    public class MinFilterGroupResponse
    {
        public MinFilterGroupResponse(IEnumerable<MinItemGroupResponse> fullGroups,
            IEnumerable<MinItemGroupResponse> fillingGroups)
        {
            FullGroups = fullGroups;
            FillingGroups = fillingGroups;
        }

        public IEnumerable<MinItemGroupResponse> FillingGroups { get; set; }
        public IEnumerable<MinItemGroupResponse> FullGroups { get; set; }
    }
}