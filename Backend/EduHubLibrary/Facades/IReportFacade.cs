using EduHubLibrary.Domain.NotificationService;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Facades
{
    public interface IReportFacade
    {
        void Report(int senderId, int suspectedId, string brokenRule);
        IEnumerable<Event> GetAll();
    }
}
