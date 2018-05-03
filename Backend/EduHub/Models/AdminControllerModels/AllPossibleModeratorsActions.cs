using EduHub.Models.NotificationsModels;

namespace EduHub.Models.AdminControllerModels
{
    public class AllPossibleModeratorsActions
    {
        public AllPossibleModeratorsActions(SanctionsAppliedForAdminResponse sanctionsAppliedForAdminResponse,
            SanctionsCancelledForAdminResponse sanctionsCancelledForAdminResponse)
        {
            SanctionsAppliedForAdminResponse = sanctionsAppliedForAdminResponse;
            SanctionsCancelledForAdminResponse = sanctionsCancelledForAdminResponse;
        }

        public SanctionsAppliedForAdminResponse SanctionsAppliedForAdminResponse { get; }
        public SanctionsCancelledForAdminResponse SanctionsCancelledForAdminResponse { get; }
    }
}