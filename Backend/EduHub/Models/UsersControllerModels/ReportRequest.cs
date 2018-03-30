using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models.UsersControllerModels
{
    public class ReportRequest
    {
        public ReportRequest(string brokenRule)
        {
            BrokenRule = brokenRule;
        }
        
        public string BrokenRule { get; set; }
    }
}
