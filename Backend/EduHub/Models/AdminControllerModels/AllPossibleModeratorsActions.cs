using EduHub.Models.NotificationsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models.AdminControllerModels
{
    public class AllPossibleModeratorsActions
    {
        public AllPossibleModeratorsActions(SanctionsAppliedForAdminResponse sanctionsAppliedForAdminResponse, SanctionsCancelledForAdminResponse sanctionsCancelledForAdminResponse)
        {
            SanctionsAppliedForAdminResponse = sanctionsAppliedForAdminResponse;
            SanctionsCancelledForAdminResponse = sanctionsCancelledForAdminResponse;
        }

        public SanctionsAppliedForAdminResponse SanctionsAppliedForAdminResponse { get; }
        public SanctionsCancelledForAdminResponse SanctionsCancelledForAdminResponse { get; }
    }
}
