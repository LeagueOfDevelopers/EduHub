using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Facades.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Facades
{
    public interface IReportFacade
    {
        void Report(int senderId, int suspectedId, string reason, string description);
        IEnumerable<ReportView> GetAll();
    }
}
