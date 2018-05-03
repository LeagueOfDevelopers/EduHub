namespace EduHubLibrary.Mailing.MessageModels
{
    public class CurriculumSuggestedMessage
    {
        public CurriculumSuggestedMessage(string curriculumLink, string groupTitle, string receiverName)
        {
            CurriculumLink = curriculumLink;
            GroupTitle = groupTitle;
            ReceiverName = receiverName;
        }

        public string CurriculumLink { get; }
        public string GroupTitle { get; }
        public string ReceiverName { get; }
    }
}