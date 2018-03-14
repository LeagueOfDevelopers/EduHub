using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Facades
{
    public interface ISanctionFacade
    {
        void AddSanction(string brokenRule, int userId, int moderatorId, SanctionType type);
        void CancelSanction(int sanctionId);
        IEnumerable<Sanction> GetAll();
        IEnumerable<Sanction> GetAllOfUser(int userId);
        IEnumerable<Sanction> GetAllOfModerator(int moderatorId);
    }
}
