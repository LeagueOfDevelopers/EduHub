using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Mailing.MessageModels
{
    public class ReportMessage
    {
        public ReportMessage(string senderName, string suspectedName, string brokenRule)
        {
            SenderName = senderName;
            SuspectedName = suspectedName;
            BrokenRule = brokenRule;
        }

        public string SenderName { get; }
        public string SuspectedName { get; }
        public string BrokenRule { get; }
    }
}
