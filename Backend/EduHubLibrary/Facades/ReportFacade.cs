using System;
using System.Collections.Generic;
using System.Text;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Events;
using System.Linq;
using EduHubLibrary.Facades.Views;
using Newtonsoft.Json;

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

        public IEnumerable<ReportView> GetAll()
        {
            var reportsEvents = _eventRepository.GetAllEvents().Where(e => e.EventType.Equals(EventType.ReportMessage));
            var reports = new List<ReportView>();
            reportsEvents.ToList().ForEach(r =>
            {
                var report = JsonConvert.DeserializeObject<ReportMessageEvent>(r.EventInfo);
                reports.Add(new ReportView(report.SenderName, report.SuspectedName, report.Reason, report.Description));
            });

            return reports;
        }

        public void Report(int senderId, int suspectedId, string reason, string description)
        {
            var sender = _userRepository.GetUserById(senderId);
            var suspected = _userRepository.GetUserById(suspectedId);
            _publisher.PublishEvent(new ReportMessageEvent(sender.UserProfile.Name, suspected.UserProfile.Name, reason, description));
        }

        private readonly IUserRepository _userRepository;
        private readonly IEventRepository _eventRepository;
        private readonly IEventPublisher _publisher;
    }
}
