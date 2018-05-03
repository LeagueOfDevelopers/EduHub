using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Domain.NotificationService.Notifications;
using EduHubLibrary.EventBus.EventTypes;

namespace EduHubLibrary.Domain.Consumers
{
    public class CurriculumEventConsumer : IEventConsumer<CurriculumAcceptedEvent>,
        IEventConsumer<CurriculumSuggestedEvent>,
        IEventConsumer<CurriculumDeclinedEvent>
    {
        private readonly INotificationsDistributor _distributor;
        private readonly IEventRepository _eventRepository;

        public CurriculumEventConsumer(INotificationsDistributor distributor, IEventRepository eventRepository)
        {
            _distributor = distributor;
            _eventRepository = eventRepository;
        }

        public void Consume(CurriculumAcceptedEvent @event)
        {
            _distributor.NotifyGroup(@event.GroupId, new CurriculumAcceptedNotification(@event.GroupTitle));
            _eventRepository.AddEvent(new Event(@event));
        }

        public void Consume(CurriculumDeclinedEvent @event)
        {
            _distributor.NotifyTeacher(@event.GroupId,
                new CurriculumDeclinedNotification(@event.GroupTitle, @event.DeclinedName));
            _eventRepository.AddEvent(new Event(@event));
        }

        public void Consume(CurriculumSuggestedEvent @event)
        {
            _distributor.NotifyGroup(@event.GroupId,
                new CurriculumSuggestedNotification(@event.CurriculumLink, @event.GroupTitle));
            _eventRepository.AddEvent(new Event(@event));
        }
    }
}