using EduHubLibrary.Domain;

namespace EduHubLibrary.Mailing.MessageModels
{
    public class SanctionsAppliedToAdminMessage
    {
        public SanctionsAppliedToAdminMessage(string brokenRule, SanctionType sanctionType, string userName,
            string receiverName)
        {
            BrokenRule = brokenRule;
            SanctionType = sanctionType;
            UserName = userName;
            ReceiverName = receiverName;
        }

        public string BrokenRule { get; }
        public SanctionType SanctionType { get; }
        public string UserName { get; }
        public string ReceiverName { get; }
    }
}