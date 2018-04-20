using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Facades.Views
{
    public class ReportView
    {
        public ReportView(string senderName, string suspectedName, string reason, string description)
        {
            SenderName = senderName;
            SuspectedName = suspectedName;
            Reason = reason;
            Description = description;
        }

        public string SenderName { get; }
        public string SuspectedName { get; }
        public string Reason { get; }
        public string Description { get; }
    }
}
