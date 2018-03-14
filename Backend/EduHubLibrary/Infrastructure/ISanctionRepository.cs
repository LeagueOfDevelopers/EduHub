using EduHubLibrary.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubLibrary.Infrastructure
{
    public interface ISanctionRepository
    {
        void Add(Sanction sanction);
        Sanction Get(int id);
        IEnumerable<Sanction> GetAll();
        IEnumerable<Sanction> GetAllOfUser(int userId);
        IEnumerable<Sanction> GetAllOfModerator(int moderatorId);
        void Update(Sanction sanction);
    }
}
