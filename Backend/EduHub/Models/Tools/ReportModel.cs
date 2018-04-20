using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models.Tools
{
    public class ReportModel
    {
        public ReportModel(string senderName, string suspectedName, string reason, string description)
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
