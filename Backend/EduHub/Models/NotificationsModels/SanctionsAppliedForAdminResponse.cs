using EduHubLibrary.Domain;

namespace EduHub.Models.NotificationsModels
{
    public class SanctionsAppliedForAdminResponse
    {
        public SanctionsAppliedForAdminResponse(string brokenRule, SanctionType sanctionType, string username)
        {
            BrokenRule = brokenRule;
            SanctionType = sanctionType;
            Username = username;
        }

        public string BrokenRule { get; }
        public SanctionType SanctionType { get; }
        public string Username { get; }
    }
}