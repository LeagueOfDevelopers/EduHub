using EduHubLibrary.Domain;
using EduHubLibrary.Domain.NotificationService;

namespace EduHubLibrary.EventBus.EventTypes
{
    public class SanctionCancelledEvent : EventInfoBase
    {
        public SanctionCancelledEvent(string brokenRule, SanctionType sanctionType, string username, int userId)
        {
            BrokenRule = brokenRule;
            SanctionType = sanctionType;
            Username = username;
            UserId = userId;
        }

        public string BrokenRule { get; }
        public SanctionType SanctionType { get; }
        public string Username { get; }
        public int UserId { get; }

        public override EventType GetEventType()
        {
            return EventType.SanctionCancelled;
        }
    }
}