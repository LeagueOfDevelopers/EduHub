using EduHubLibrary.Domain.Events;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Domain.NotificationService.Notifications;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Consumers
{
    public class AdminsEventConsumer : IEventConsumer<ReportMessageEvent>, IEventConsumer<SanctionsAppliedEvent>
    {
        public AdminsEventConsumer(INotificationsDistributor distributor, IEventRepository eventRepository)
        {
            _distributor = distributor;
            _eventRepository = eventRepository;
        }

        public void Consume(ReportMessageEvent @event)
        {
            _distributor.NotifyAdmins(new ReportMessageNotification(@event.SenderName, @event.SuspectedName, @event.BrokenRule));

            _eventRepository.AddEvent(new Event(@event));
        }

        public void Consume(SanctionsAppliedEvent @event)
        {
            _distributor.NotifyAdmins(new SanctionAppliedToAdminNotification(@event.BrokenRule, @event.SanctionType, @event.Username));
            _distributor.NotifyPerson(@event.UserId, new SanctionsAppliedToUserNotification(@event.BrokenRule, @event.SanctionType));
        }

        private readonly INotificationsDistributor _distributor;
        private readonly IEventRepository _eventRepository;
    }
}
