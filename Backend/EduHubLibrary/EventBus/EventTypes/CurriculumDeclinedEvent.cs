using EduHubLibrary.Domain.NotificationService;

namespace EduHubLibrary.EventBus.EventTypes
{
    public class CurriculumDeclinedEvent : EventInfoBase
    {
        public CurriculumDeclinedEvent(string groupTitle, int groupId, string declinedName)
        {
            GroupTitle = groupTitle;
            GroupId = groupId;
            DeclinedName = declinedName;
        }

        public string GroupTitle { get; }
        public int GroupId { get; }
        public string DeclinedName { get; }

        public override EventType GetEventType()
        {
            return EventType.CurriculumDeclined;
        }
    }
}