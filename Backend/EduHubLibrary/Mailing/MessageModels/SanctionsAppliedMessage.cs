using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Mailing.MessageModels
{
    public class SanctionsAppliedMessage
    {
        public SanctionsAppliedMessage(string brokenRule, SanctionType sanctionType, bool isReceiverAdmin)
        {
            BrokenRule = brokenRule;
            SanctionType = sanctionType;
            IsReceiverAdmin = isReceiverAdmin;
        }

        public string BrokenRule { get; }
        public SanctionType SanctionType { get; }
        public bool IsReceiverAdmin { get; }
    }
}
