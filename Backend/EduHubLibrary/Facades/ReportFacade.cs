using System;
using System.Collections.Generic;
using System.Text;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Domain;
using System.Linq;
using EduHubLibrary.Facades.Views;
using Newtonsoft.Json;
using EduHubLibrary.EventBus.EventTypes;

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
                reports.Add(new ReportView(r.Id, report.SenderName, report.SuspectedName, report.Reason, report.Description));
            });

            return reports;
        }

        public ReportView Get(int reportId)
        {
            var report = _eventRepository.GetEvent(reportId);
            var reportEvent = JsonConvert.DeserializeObject<ReportMessageEvent>(report.EventInfo);
            var reportView = new ReportView(report.Id, reportEvent.SenderName, reportEvent.SuspectedName,
                reportEvent.Reason, reportEvent.Description);

            return reportView;
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
