using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Mailing.MessageModels
{
    public class SanctionsCancelledToAdminMessage
    {
        public SanctionsCancelledToAdminMessage(string brokenRule, SanctionType sanctionType, string username, string receiverName)
        {
            BrokenRule = brokenRule;
            SanctionType = sanctionType;
            Username = username;
            ReceiverName = receiverName;
        }

        public string BrokenRule { get; }
        public SanctionType SanctionType { get; }
        public string Username { get; }
        public string ReceiverName { get; }
    }
}
