﻿using System.Collections.Generic;
using EduHubLibrary.Domain;

namespace EduHubLibrary.Infrastructure
{
    public interface ISanctionRepository
    {
        void Add(Sanction sanction);
        Sanction Get(int id);
        IEnumerable<Sanction> GetAll();
        IEnumerable<Sanction> GetAllOfUser(int userId);
        IEnumerable<Sanction> GetAllActive();
        void Update(Sanction sanction);
    }
}