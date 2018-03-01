namespace EduHub.Models.NotificationsModels
{
    public class SanctionsAppliedResponse
    {
        public SanctionsAppliedResponse(string brokenRule, string sanctionType)
        {
            BrokenRule = brokenRule;
            SanctionType = sanctionType;
        }

        public string BrokenRule { get; }
        public string SanctionType { get; }
    }
}