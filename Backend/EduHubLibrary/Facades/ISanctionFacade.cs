using System;
using System.Collections.Generic;
using EduHubLibrary.Domain;
using EduHubLibrary.Facades.Views;

namespace EduHubLibrary.Facades
{
    public interface ISanctionFacade
    {
        int AddSanction(string brokenRule, int userId, int moderatorId, SanctionType type);

        int AddSanction(string brokenRule, int userId, int moderatorId, SanctionType type,
            DateTimeOffset expirationDate);

        void CancelSanction(int sanctionId);
        IEnumerable<SanctionView> GetAll();
        IEnumerable<SanctionView> GetAllOfUser(int userId);
        IEnumerable<SanctionView> GetAllActiveOfUser(int userId);
        IEnumerable<SanctionView> GetAllActive();
    }
}