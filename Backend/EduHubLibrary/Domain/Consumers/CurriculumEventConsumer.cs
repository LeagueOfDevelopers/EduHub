using EduHubLibrary.Domain.Events;
using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Consumers
{
    public class CurriculumEventConsumer : IEventConsumer<CurriculumAcceptedEvent>, IEventConsumer<CurriculumSuggestedEvent>,
        IEventConsumer<CurriculumDeclinedEvent>
    {
        public CurriculumEventConsumer(INotificationsDistributor distributor)
        {
            _distributor = distributor;
        }

        public void Consume(CurriculumAcceptedEvent @event)
        {
            _distributor.NotifyGroup(@event.GroupId, @event);
        }

        public void Consume(CurriculumSuggestedEvent @event)
        {
            _distributor.NotifyGroup(@event.GroupId, @event);
        }

        public void Consume(CurriculumDeclinedEvent @event)
        {
            _distributor.NotifyTeacher(@event.GroupId, @event);
        }

        private readonly INotificationsDistributor _distributor;
    }
}
