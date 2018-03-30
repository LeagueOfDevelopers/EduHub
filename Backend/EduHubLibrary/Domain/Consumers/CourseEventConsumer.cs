using EduHubLibrary.Domain.Events;
using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Consumers
{
    public class CourseEventConsumer : IEventConsumer<TeacherFoundEvent>, IEventConsumer<CourseFinishedEvent>, 
        IEventConsumer<ReviewReceivedEvent>
    { 
        public CourseEventConsumer(INotificationsDistributor distributor)
        {
            _distributor = distributor;
        }

        public void Consume(TeacherFoundEvent @event)
        {
            _distributor.NotifyGroup(@event.GroupId, @event);
        }

        public void Consume(CourseFinishedEvent @event)
        {
            _distributor.NotifyGroup(@event.GroupId, @event);
        }

        public void Consume(ReviewReceivedEvent @event)
        {
            _distributor.NotifyTeacher(@event.GroupId, @event);
        }

        private readonly INotificationsDistributor _distributor;
    }
}
