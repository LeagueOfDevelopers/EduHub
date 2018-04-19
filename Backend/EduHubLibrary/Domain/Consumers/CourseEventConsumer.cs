using EduHubLibrary.Domain.Events;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Domain.NotificationService.Notifications;
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
            _distributor.NotifyGroup(@event.GroupId, new TeacherFoundNotification(@event.TeacherName, @event.GroupTitle));
        }

        public void Consume(CourseFinishedEvent @event)
        {
            _distributor.NotifyGroup(@event.GroupId, new CourseFinishedNotification(@event.GroupTitle, @event.TeacherName));
        }

        public void Consume(ReviewReceivedEvent @event)
        {
            _distributor.NotifyTeacher(@event.GroupId, new ReviewReceivedNotification(@event.GroupTitle, @event.ReviewerName, @event.ReviewType));
        }

        private readonly INotificationsDistributor _distributor;
    }
}
