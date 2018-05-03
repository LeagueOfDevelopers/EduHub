using EduHubLibrary.Domain;

namespace EduHubLibrary.Mailing.MessageModels
{
    public class SanctionsCancelledToUserMessage
    {
        public SanctionsCancelledToUserMessage(string brokenRule, SanctionType sanctionType, string receiverName)
        {
            BrokenRule = brokenRule;
            SanctionType = sanctionType;
            ReceiverName = receiverName;
        }

        public string BrokenRule { get; }
        public SanctionType SanctionType { get; }
        public string ReceiverName { get; }
    }
}