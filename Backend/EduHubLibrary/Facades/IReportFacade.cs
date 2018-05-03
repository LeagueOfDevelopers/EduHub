using System.Collections.Generic;
using EduHubLibrary.Facades.Views;

namespace EduHubLibrary.Facades
{
    public interface IReportFacade
    {
        void Report(int senderId, int suspectedId, string reason, string description);
        IEnumerable<ReportView> GetAll();
        ReportView Get(int reportId);
    }
}