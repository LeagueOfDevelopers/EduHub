﻿using System;
using System.Collections.Generic;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Domain.Tools;
using EnsureThat;
using EduHubLibrary.Infrastructure;
using System.Linq;

namespace EduHubLibrary.Facades
{
    public class UserEditFacade : IUserEditFacade
    {
        private readonly IFileRepository _fileRepository;
        private readonly ISanctionRepository _sanctionRepository;
        private readonly IUserRepository _userRepository;

        public UserEditFacade(IUserRepository userRepository,
            IFileRepository fileRepository, ISanctionRepository sanctionRepository)
        {
            _userRepository = userRepository;
            _fileRepository = fileRepository;
            _sanctionRepository = sanctionRepository;
        }

        public void EditName(int userId, string newName)
        {
            Ensure.Bool.IsFalse(_sanctionRepository.GetAllOfUser(userId).ToList()
                .Exists(s => s.Type.Equals(SanctionType.NotAllowToEditProfile)), nameof(EditName),
                opt => opt.WithException(new ActionIsNotAllowWithSanctionsException(SanctionType.NotAllowToEditProfile)));

            var currentUser = _userRepository.GetUserById(userId);
            currentUser.UserProfile.Name = Ensure.Any.IsNotNull(newName);
            _userRepository.Update(currentUser);
        }

        public void EditAboutUser(int userId, string newAboutUser)
        {
            var currentUser = _userRepository.GetUserById(userId);
            currentUser.UserProfile.AboutUser = Ensure.Any.IsNotNull(newAboutUser);
            _userRepository.Update(currentUser);
        }

        public void EditGender(int userId, Gender gender)
        {
            var currentUser = _userRepository.GetUserById(userId);
            currentUser.UserProfile.Gender = gender;
            _userRepository.Update(currentUser);
        }

        public void EditAvatarLink(int userId, string newAvatarLink)
        {
            Ensure.Any.IsNotNull(newAvatarLink);
            var currentUser = _userRepository.GetUserById(userId);
            if (newAvatarLink.Length == 0)
            {
                currentUser.UserProfile.AvatarLink = newAvatarLink;
                return;
            }

            Ensure.Bool.IsTrue(_fileRepository.DoesFileExists(newAvatarLink),
                nameof(EditAboutUser),
                opt => opt.WithException(new FileDoesNotExistException()));


            currentUser.UserProfile.AvatarLink = newAvatarLink;
            _userRepository.Update(currentUser);
        }

        public void EditContacts(int userId, List<string> newContactData)
        {
            var currentUser = _userRepository.GetUserById(userId);

            Ensure.Any.IsNotNull(newContactData);

            if (newContactData.TrueForAll(d => !string.IsNullOrWhiteSpace(d)))
                currentUser.UserProfile.Contacts = newContactData;
            else throw new ArgumentException();

            _userRepository.Update(currentUser);
        }

        public void EditBirthYear(int userId, int newYear)
        {
            var currentUser = _userRepository.GetUserById(userId);

            //hardcoded value
            if (newYear > 1900 && newYear < DateTimeOffset.Now.Year || newYear == 0)
                currentUser.UserProfile.BirthYear = newYear;
            else throw new IndexOutOfRangeException();

            _userRepository.Update(currentUser);
        }

        public void BecomeTeacher(int userId)
        {
            var currentUser = _userRepository.GetUserById(userId);
            currentUser.UserProfile.IsTeacher = true;
            _userRepository.Update(currentUser);
        }

        public void StopToBeTeacher(int userId)
        {
            var currentUser = _userRepository.GetUserById(userId);
            currentUser.UserProfile.IsTeacher = false;
            _userRepository.Update(currentUser);
        }
    }
}