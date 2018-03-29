using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Events
{
    public class SanctionsAppliedEvent : EventInfoBase
    {
        public SanctionsAppliedEvent(string brokenRule, string sanctionType, int userId)
        {
            BrokenRule = brokenRule;
            SanctionType = sanctionType;
            UserId = userId;
        }

        public string BrokenRule { get; }
        public string SanctionType { get; }
        public int UserId { get; }
    }
}
