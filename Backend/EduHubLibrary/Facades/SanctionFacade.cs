using System;
using System.Collections.Generic;
using System.Text;
using EduHubLibrary.Domain;
using EduHubLibrary.Infrastructure;
using EnsureThat;
using EduHubLibrary.Common;

namespace EduHubLibrary.Facades
{
    public class SanctionFacade : ISanctionFacade
    {
        public SanctionFacade(ISanctionRepository sanctionRepository)
        {
            _sanctionRepository = sanctionRepository;
        }

        public void AddSanction(string brokenRule, int userId, int moderatorId, SanctionType type)
        {
            Ensure.String.IsNotNullOrWhiteSpace(brokenRule);
            Ensure.Any.IsNotNull(type);

            var sanction = new Sanction(brokenRule, userId, moderatorId, type);
            _sanctionRepository.Add(sanction);
        }

        public void CancelSanction(int sanctionId)
        {
            var currentSanction = _sanctionRepository.Get(sanctionId);
            currentSanction.Cancel();
            _sanctionRepository.Update(currentSanction);
        }

        public IEnumerable<Sanction> GetAll()
        {
            return _sanctionRepository.GetAll();
        }

        public IEnumerable<Sanction> GetAllOfModerator(int moderatorId)
        {
            return _sanctionRepository.GetAllOfModerator(moderatorId);
        }

        public IEnumerable<Sanction> GetAllOfUser(int userId)
        {
            return _sanctionRepository.GetAllOfUser(userId);
        }

        private readonly ISanctionRepository _sanctionRepository;
        private readonly IUserRepository _userRepository;
    }
}
