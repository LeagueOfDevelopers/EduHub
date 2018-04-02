using System;
using System.Collections.Generic;
using EduHubLibrary.Domain;

namespace EduHubLibrary.Facades
{
    public interface ISanctionFacade
    {
        int AddSanction(string brokenRule, int userId, int moderatorId, SanctionType type);

        int AddSanction(string brokenRule, int userId, int moderatorId, SanctionType type,
            DateTimeOffset expirationDate);

        void CancelSanction(int sanctionId);
        IEnumerable<Sanction> GetAll();
        IEnumerable<Sanction> GetAllOfUser(int userId);
        IEnumerable<Sanction> GetAllActiveOfUser(int userId);
        IEnumerable<Sanction> GetAllOfModerator(int moderatorId);
    }
}