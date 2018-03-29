using EduHubLibrary.Domain.NotificationService;

namespace EduHubLibrary.Domain.Events
{
    public class UsingTagEvent : EventInfoBase
    {
        public UsingTagEvent(string tag)
        {
            Tag = tag;
        }

        public string Tag { get; }
    }
}