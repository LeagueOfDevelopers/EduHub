using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models.NotificationsModels
{
    public class SanctionsCancelledForUserResponse
    {
        public SanctionsCancelledForUserResponse(string brokenRule, SanctionType sanctionType)
        {
            BrokenRule = brokenRule;
            SanctionType = sanctionType;
        }

        public string BrokenRule { get; }
        public SanctionType SanctionType { get; }
    }
}
