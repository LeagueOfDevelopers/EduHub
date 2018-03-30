using System;
using System.Collections.Generic;
using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Infrastructure;
using EnsureThat;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Domain.Events;

namespace EduHubLibrary.Facades
{
    public class SanctionFacade : ISanctionFacade
    {
        private readonly ISanctionRepository _sanctionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IEventPublisher _publisher;

        public SanctionFacade(ISanctionRepository sanctionRepository, IUserRepository userRepository, IEventPublisher publisher)
        {
            _sanctionRepository = sanctionRepository;
            _userRepository = userRepository;
            _publisher = publisher;
        }

        public int AddSanction(string brokenRule, int userId, int moderatorId, SanctionType type)
        {
            Ensure.String.IsNotNullOrWhiteSpace(brokenRule);
            Ensure.Any.IsNotNull(type);
            Ensure.Any.IsNotNull(_userRepository.GetUserById(userId), nameof(AddSanction),
                opt => opt.WithException(new UserNotFoundException(userId)));
            Ensure.Bool.IsTrue(_userRepository.GetUserById(moderatorId).Type.Equals(UserType.Moderator) ||
                               _userRepository.GetUserById(moderatorId).Type.Equals(UserType.Admin),
                nameof(AddSanction),
                opt => opt.WithException(new NotEnoughPermissionsException(moderatorId)));

            var sanction = new Sanction(brokenRule, userId, moderatorId, type);
            _sanctionRepository.Add(sanction);
            return sanction.Id;
        }

        public int AddSanction(string brokenRule, int userId, int moderatorId, SanctionType type,
            DateTimeOffset expirationDate)
        {
            Ensure.String.IsNotNullOrWhiteSpace(brokenRule);
            Ensure.Any.IsNotNull(type);
            Ensure.Any.IsNotNull(expirationDate);
            Ensure.Bool.IsTrue(expirationDate > DateTimeOffset.Now, nameof(AddSanction),
                opt => opt.WithException(new ArgumentException()));
            Ensure.Any.IsNotNull(_userRepository.GetUserById(userId), nameof(AddSanction),
                opt => opt.WithException(new UserNotFoundException(userId)));
            Ensure.Bool.IsTrue(_userRepository.GetUserById(moderatorId).Type.Equals(UserType.Moderator) ||
                               _userRepository.GetUserById(moderatorId).Type.Equals(UserType.Admin),
                nameof(AddSanction),
                opt => opt.WithException(new NotEnoughPermissionsException(moderatorId)));

            var sanction = new Sanction(brokenRule, userId, moderatorId, type, expirationDate);
            _sanctionRepository.Add(sanction);

            _publisher.PublishEvent(new SanctionsAppliedEvent(brokenRule, type, userId));

            return sanction.Id;
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
            Ensure.Bool.IsTrue(_userRepository.GetUserById(moderatorId).Type.Equals(UserType.Moderator) ||
                               _userRepository.GetUserById(moderatorId).Type.Equals(UserType.Admin),
                nameof(GetAllOfModerator),
                opt => opt.WithException(new NotEnoughPermissionsException(moderatorId)));

            return _sanctionRepository.GetAllOfModerator(moderatorId);
        }

        public IEnumerable<Sanction> GetAllOfUser(int userId)
        {
            Ensure.Any.IsNotNull(_userRepository.GetUserById(userId), nameof(GetAllOfUser),
                opt => opt.WithException(new UserNotFoundException(userId)));

            return _sanctionRepository.GetAllOfUser(userId);
        }
    }
}