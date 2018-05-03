using EduHubLibrary.Domain.NotificationService;

namespace EduHubLibrary.EventBus.EventTypes
{
    public class TeacherFoundEvent : EventInfoBase
    {
        public TeacherFoundEvent(string teacherName, string groupTitle, int groupId)
        {
            TeacherName = teacherName;
            GroupTitle = groupTitle;
            GroupId = groupId;
        }

        public string TeacherName { get; }
        public string GroupTitle { get; }
        public int GroupId { get; }

        public override EventType GetEventType()
        {
            return EventType.TeacherFound;
        }
    }
}