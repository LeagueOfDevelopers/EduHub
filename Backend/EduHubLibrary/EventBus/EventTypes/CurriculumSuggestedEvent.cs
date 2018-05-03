using EduHubLibrary.Domain.NotificationService;

namespace EduHubLibrary.EventBus.EventTypes
{
    public class CurriculumSuggestedEvent : EventInfoBase
    {
        public CurriculumSuggestedEvent(string curriculumLink, string groupTitle, int groupId)
        {
            CurriculumLink = curriculumLink;
            GroupTitle = groupTitle;
            GroupId = groupId;
        }

        public string CurriculumLink { get; }
        public string GroupTitle { get; }
        public int GroupId { get; }

        public override EventType GetEventType()
        {
            return EventType.CurriculumSuggested;
        }
    }
}