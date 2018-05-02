using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Facades.Views
{
    public class ReportView
    {
        public ReportView(int reportId, string senderName, string suspectedName, string reason, string description)
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
