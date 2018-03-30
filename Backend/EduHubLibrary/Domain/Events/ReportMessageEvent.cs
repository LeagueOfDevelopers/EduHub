using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Events
{
    public class ReportMessageEvent : EventInfoBase
    {
        public ReportMessageEvent(string senderName, string suspectedName, string brokenRule)
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
