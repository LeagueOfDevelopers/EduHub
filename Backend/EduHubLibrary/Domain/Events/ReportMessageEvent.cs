using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Events
{
    public class ReportMessageEvent : EventInfoBase
    {
        public ReportMessageEvent(string senderName,
            Guid senderId, string suspectedName, Guid suspectedId, string brokenRule)
        {
            SenderName = senderName;
            SenderId = senderId;
            SuspectedName = suspectedName;
            SuspectedId = suspectedId;
            BrokenRule = brokenRule;
        }

        public string SenderName { get; }
        public Guid SenderId { get; }
        public string SuspectedName { get; }
        public Guid SuspectedId { get; }
        public string BrokenRule { get; }
    }
}
