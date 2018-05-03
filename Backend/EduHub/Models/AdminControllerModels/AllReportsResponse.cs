using System.Collections.Generic;
using EduHub.Models.Tools;

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