using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Domain.NotificationService.Notifications;
using EduHubLibrary.EventBus.EventTypes;
using EduHubLibrary.NotificationService.NotificationTypes;

namespace EduHubLibrary.Domain.Consumers
{
    public class AdminsEventConsumer : IEventConsumer<ReportMessageEvent>, IEventConsumer<SanctionsAppliedEvent>,
        IEventConsumer<SanctionCancelledEvent>
    {
        private readonly INotificationsDistributor _distributor;
        private readonly IEventRepository _eventRepository;

        public AdminsEventConsumer(INotificationsDistributor distributor, IEventRepository eventRepository)
        {
            _distributor = distributor;
            _eventRepository = eventRepository;
        }

        public void Consume(ReportMessageEvent @event)
        {
            _distributor.NotifyAdmins(new ReportMessageNotification(@event.SenderName, @event.SuspectedName,
                @event.Reason, @event.Description));

            _eventRepository.AddEvent(new Event(@event));
        }

        public void Consume(SanctionCancelledEvent @event)
        {
            _distributor.NotifyAdmins(
                new SanctionCancelledToAdminNotification(@event.BrokenRule, @event.SanctionType, @event.Username));
            _distributor.NotifyPerson(@event.UserId,
                new SanctionsCancelledToUserNotification(@event.BrokenRule, @event.SanctionType));

            _eventRepository.AddEvent(new Event(@event));
        }

        public void Consume(SanctionsAppliedEvent @event)
        {
            _distributor.NotifyAdmins(
                new SanctionAppliedToAdminNotification(@event.BrokenRule, @event.SanctionType, @event.Username));
            _distributor.NotifyPerson(@event.UserId,
                new SanctionsAppliedToUserNotification(@event.BrokenRule, @event.SanctionType));

            _eventRepository.AddEvent(new Event(@event));
        }
    }
}