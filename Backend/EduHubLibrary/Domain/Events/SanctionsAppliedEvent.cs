using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Events
{
    public class SanctionsAppliedEvent : EventInfoBase
    {
        public SanctionsAppliedEvent(string brokenRule, SanctionType sanctionType, int userId)
        {
            BrokenRule = brokenRule;
            SanctionType = sanctionType;
            UserId = userId;
        }

        public string BrokenRule { get; }
        public SanctionType SanctionType { get; }
        public int UserId { get; }

        public override EventType GetEventType()
        {
            return EventType.SanctionsApplied;
        }
    }
}
