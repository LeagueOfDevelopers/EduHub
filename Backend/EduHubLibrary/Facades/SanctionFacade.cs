﻿using System;
using System.Collections.Generic;
using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Infrastructure;
using EnsureThat;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Domain.Events;
using System.Linq;
using EduHubLibrary.Facades.Views;

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

            var suspectedUser = _userRepository.GetUserById(userId);
            var sanction = new Sanction(brokenRule, userId, moderatorId, type);
            _sanctionRepository.Add(sanction);

            _publisher.PublishEvent(new SanctionsAppliedEvent(brokenRule, type, suspectedUser.UserProfile.Name, userId));

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
                nameof(AddSanction), opt => opt.WithException(new NotEnoughPermissionsException(moderatorId)));
            var suspectedUser = _userRepository.GetUserById(userId);
            var sanction = new Sanction(brokenRule, userId, moderatorId, type, expirationDate);
            _sanctionRepository.Add(sanction);

            _publisher.PublishEvent(new SanctionsAppliedEvent(brokenRule, type, suspectedUser.UserProfile.Name, userId));

            return sanction.Id;
        }

        public void CancelSanction(int sanctionId)
        {
            var currentSanction = _sanctionRepository.Get(sanctionId);
            currentSanction.Cancel();
            _sanctionRepository.Update(currentSanction);
        }

        public IEnumerable<SanctionView> GetAll()
        {
            var sanctions = new List<SanctionView>();
            _sanctionRepository.GetAll().ToList().ForEach(s =>
            {
                var username = _userRepository.GetUserById(s.UserId).UserProfile.Name;
                sanctions.Add(new SanctionView(s.Id, s.BrokenRule, s.UserId, username, s.ModeratorId, s.IsTemporary,
                    s.ExpirationDate, s.Type, s.IsActive));
            });

            return sanctions;
        }

        public IEnumerable<SanctionView> GetAllActiveOfUser(int userId)
        {
            Ensure.Any.IsNotNull(_userRepository.GetUserById(userId), nameof(GetAllOfUser),
                opt => opt.WithException(new UserNotFoundException(userId)));

            var username = _userRepository.GetUserById(userId).UserProfile.Name;
            var sanctions = new List<SanctionView>();

            _sanctionRepository.GetAllOfUser(userId).Where(s => s.IsActive).ToList().ForEach(s =>
            {
                sanctions.Add(new SanctionView(s.Id, s.BrokenRule, s.UserId, username, s.ModeratorId, s.IsTemporary,
                    s.ExpirationDate, s.Type, s.IsActive));
            });

            return sanctions;
        }

        public IEnumerable<SanctionView> GetAllActive()
        {
            var sanctions = new List<SanctionView>();
            _sanctionRepository.GetAllActive().ToList().ForEach(s =>
            {
                var username = _userRepository.GetUserById(s.UserId).UserProfile.Name;
                sanctions.Add(new SanctionView(s.Id, s.BrokenRule, s.UserId, username, s.ModeratorId, s.IsTemporary,
                    s.ExpirationDate, s.Type, s.IsActive));
             });

            return sanctions;
        }

        public IEnumerable<SanctionView> GetAllOfUser(int userId)
        {
            Ensure.Any.IsNotNull(_userRepository.GetUserById(userId), nameof(GetAllOfUser),
                opt => opt.WithException(new UserNotFoundException(userId)));

            var username = _userRepository.GetUserById(userId).UserProfile.Name;
            var sanctions = new List<SanctionView>();

            _sanctionRepository.GetAllOfUser(userId).ToList().ForEach(s =>
            {
                sanctions.Add(new SanctionView(s.Id, s.BrokenRule, s.UserId, username, s.ModeratorId, s.IsTemporary,
                    s.ExpirationDate, s.Type, s.IsActive));
            });

            return sanctions;
        }
    }
}