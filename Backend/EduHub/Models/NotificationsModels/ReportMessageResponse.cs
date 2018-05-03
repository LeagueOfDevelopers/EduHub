namespace EduHub.Models.NotificationsModels
{
    public class ReportMessageResponse
    {
        public ReportMessageResponse(string senderName, string suspectedName, string brokenRule)
        {
            SenderName = senderName;
            SuspectedName = suspectedName;
            BrokenRule = brokenRule;
        }

        public string SenderName { get; }
        public string SuspectedName { get; }
        public string BrokenRule { get; }
    }
}