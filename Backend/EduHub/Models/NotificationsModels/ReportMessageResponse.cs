using System;

namespace EduHub.Models.NotificationsModels
{
    public class ReportMessageResponse
    {
        public ReportMessageResponse(string senderName,
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