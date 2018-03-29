using EduHubLibrary.Domain.NotificationService;

namespace EduHubLibrary.Domain.Events
{
    public class NewCurriculumEvent : EventInfoBase
    {
        public NewCurriculumEvent(int groupId, string curriculum)
        {
            GroupId = groupId;
            Curriculum = curriculum;
        }

        public int GroupId { get; }
        public string Curriculum { get; }

        public override EventType GetEventType()
        {
            return EventType.NewCurriculumEvent;
        }
    }
}