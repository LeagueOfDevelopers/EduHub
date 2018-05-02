using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models.Tools
{
    public class ReportModel
    {
        public ReportModel(int reportId, string senderName, string suspectedName, string reason, string description)
        {
            ReportId = reportId;
            SenderName = senderName;
            SuspectedName = suspectedName;
            Reason = reason;
            Description = description;
        }

        public int ReportId { get; }
        public string SenderName { get; }
        public string SuspectedName { get; }
        public string Reason { get; }
        public string Description { get; }
    }
}
