using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Mailing.MessageModels
{
    public class ReportMessage
    {
        public ReportMessage(string senderName, string suspectedName, string reason, string description, string receiverName)
        {
            SenderName = senderName;
            SuspectedName = suspectedName;
            Reason = reason;
            Description = description;
            ReceiverName = receiverName;
        }

        public string SenderName { get; }
        public string SuspectedName { get; }
        public string Reason { get; }
        public string Description { get; }
        public string ReceiverName { get; }
    }
}
