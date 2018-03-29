using EduHubLibrary.Domain.Events;
using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Consumers
{
    public class AdminsEventConsumer : IEventConsumer<ReportMessageEvent>, IEventConsumer<SanctionsAppliedEvent>
    {
        public AdminsEventConsumer(INotificationsDistributor distributor)
        {
            _distributor = distributor;
        }

        public void Consume(ReportMessageEvent @event)
        {
            _distributor.NotifyAdmins(@event);
        }

        public void Consume(SanctionsAppliedEvent @event)
        {
            _distributor.NotifyAdmins(@event);
            _distributor.NotifyPerson(@event.UserId, @event);
        }

        private readonly INotificationsDistributor _distributor;
    }
}
