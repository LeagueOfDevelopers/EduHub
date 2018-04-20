using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Events
{
    public class ReportMessageEvent : EventInfoBase
    {
        public ReportMessageEvent(string senderName, string suspectedName, string reason, string description)
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

        public override EventType GetEventType()
        {
            return EventType.ReportMessage;
        }
    }
}
