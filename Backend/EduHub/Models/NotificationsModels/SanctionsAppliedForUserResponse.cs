using EduHubLibrary.Domain;

namespace EduHub.Models.NotificationsModels
{
    public class SanctionsAppliedForUserResponse
    {
        public SanctionsAppliedForUserResponse(string brokenRule, SanctionType sanctionType)
        {
            BrokenRule = brokenRule;
            SanctionType = sanctionType;
        }

        public string BrokenRule { get; }
        public SanctionType SanctionType { get; }
    }
}