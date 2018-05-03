using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Domain.NotificationService.Notifications;
using EduHubLibrary.EventBus.EventTypes;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Domain.Consumers
{
    public class CourseEventConsumer : IEventConsumer<TeacherFoundEvent>, IEventConsumer<CourseFinishedEvent>, 
        IEventConsumer<ReviewReceivedEvent>
    { 
        public CourseEventConsumer(INotificationsDistributor distributor, IEventRepository eventRepository)
        {
            _distributor = distributor;
            _eventRepository = eventRepository;
        }

        public void Consume(TeacherFoundEvent @event)
        {
            _distributor.NotifyGroup(@event.GroupId, new TeacherFoundNotification(@event.TeacherName, @event.GroupTitle));
            _eventRepository.AddEvent(new Event(@event));
        }

        public void Consume(CourseFinishedEvent @event)
        {
            _distributor.NotifyGroup(@event.GroupId, new CourseFinishedNotification(@event.GroupTitle, @event.TeacherName));
            _eventRepository.AddEvent(new Event(@event));
        }

        public void Consume(ReviewReceivedEvent @event)
        {
            _distributor.NotifyTeacher(@event.GroupId, new ReviewReceivedNotification(@event.GroupTitle, @event.ReviewerName, @event.ReviewType));
            _eventRepository.AddEvent(new Event(@event));
        }

        private readonly INotificationsDistributor _distributor;
        private readonly IEventRepository _eventRepository;
    }
}
