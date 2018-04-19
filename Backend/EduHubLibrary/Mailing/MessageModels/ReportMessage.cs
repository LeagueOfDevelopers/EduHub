using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Mailing.MessageModels
{
    public class ReportMessage
    {
        public ReportMessage(string senderName, string suspectedName, string brokenRule, string receiverName)
        {
            SenderName = senderName;
            SuspectedName = suspectedName;
            BrokenRule = brokenRule;
            ReceiverName = receiverName;
        }

        public string SenderName { get; }
        public string SuspectedName { get; }
        public string BrokenRule { get; }
        public string ReceiverName { get; }
    }
}
