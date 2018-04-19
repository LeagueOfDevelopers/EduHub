using EduHubLibrary.Domain.Events;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Domain.NotificationService.Notifications;
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
            _distributor.NotifyGroup(@event.GroupId, new CurriculumAcceptedNotification(@event.GroupTitle));
        }

        public void Consume(CurriculumSuggestedEvent @event)
        {
            _distributor.NotifyGroup(@event.GroupId, new CurriculumSuggestedNotification(@event.CurriculumLink, @event.GroupTitle));
        }

        public void Consume(CurriculumDeclinedEvent @event)
        {
            _distributor.NotifyTeacher(@event.GroupId, new CurriculumDeclinedNotification(@event.GroupTitle, @event.DeclinedName));
        }

        private readonly INotificationsDistributor _distributor;
    }
}
