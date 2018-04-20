using EduHub.Models.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models.AdminControllerModels
{
    public class AllReportsResponse
    {
        public AllReportsResponse(IEnumerable<ReportModel> reports)
        {
            Reports = reports;
        }

        public IEnumerable<ReportModel> Reports { get; }
    }
}
