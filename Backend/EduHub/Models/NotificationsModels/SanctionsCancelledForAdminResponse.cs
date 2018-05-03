using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHub.Models.NotificationsModels
{
    public class SanctionsCancelledForAdminResponse
    {
        public SanctionsCancelledForAdminResponse(string brokenRule, SanctionType sanctionType, string username)
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
