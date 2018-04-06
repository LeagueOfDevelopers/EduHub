using System;
using System.Collections.Generic;
using System.Text;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Events;
using System.Linq;

namespace EduHubLibrary.Facades
{
    public class ReportFacade : IReportFacade
    {
        public ReportFacade(IUserRepository userRepository, IEventRepository eventRepository, IEventPublisher publisher)
        {
            _userRepository = userRepository;
            _eventRepository = eventRepository;
            _publisher = publisher;
        }

        public IEnumerable<Event> GetAll()
        {
            return _eventRepository.GetAllEvents().Where(e => e.EventType.Equals(EventType.ReportMessage));
        }

        public void Report(int senderId, int suspectedId, string brokenRule)
        {
            var sender = _userRepository.GetUserById(senderId);
            var suspected = _userRepository.GetUserById(suspectedId);
            _publisher.PublishEvent(new ReportMessageEvent(sender.UserProfile.Name, suspected.UserProfile.Name, brokenRule));
        }

        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IEventPublisher _publisher;
    }
}
