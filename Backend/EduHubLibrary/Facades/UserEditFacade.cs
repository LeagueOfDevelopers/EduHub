using System;
using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Domain.NotificationService.UserSettings;
using EduHubLibrary.Domain.Tools;
using EduHubLibrary.Infrastructure;
using EnsureThat;
using EduHubLibrary.Domain.NotificationService.Notifications;

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
            CheckSanctions(userId, SanctionType.NotAllowToEditProfile);

            var currentUser = _userRepository.GetUserById(userId);
            currentUser.UserProfile.Name = Ensure.Any.IsNotNull(newName);
            _userRepository.Update(currentUser);
        }

        public void EditAboutUser(int userId, string newAboutUser)
        {
            CheckSanctions(userId, SanctionType.NotAllowToEditProfile);

            var currentUser = _userRepository.GetUserById(userId);
            currentUser.UserProfile.AboutUser = Ensure.Any.IsNotNull(newAboutUser);
            _userRepository.Update(currentUser);
        }

        public void EditGender(int userId, Gender gender)
        {
            CheckSanctions(userId, SanctionType.NotAllowToEditProfile);

            var currentUser = _userRepository.GetUserById(userId);
            currentUser.UserProfile.Gender = gender;
            _userRepository.Update(currentUser);
        }

        public void EditAvatarLink(int userId, string newAvatarLink)
        {
            CheckSanctions(userId, SanctionType.NotAllowToEditProfile);

            Ensure.Any.IsNotNull(newAvatarLink);
            var currentUser = _userRepository.GetUserById(userId);
            if (newAvatarLink.Length == 0)
            {
                currentUser.UserProfile.AvatarLink = newAvatarLink;
                _userRepository.Update(currentUser);
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
            CheckSanctions(userId, SanctionType.NotAllowToEditProfile);

            var currentUser = _userRepository.GetUserById(userId);
            Ensure.Any.IsNotNull(newContactData);

            if (newContactData.TrueForAll(d => !string.IsNullOrWhiteSpace(d)))
                currentUser.UserProfile.Contacts = newContactData;
            else throw new ArgumentException();

            _userRepository.Update(currentUser);
        }

        public void EditBirthYear(int userId, int newYear)
        {
            CheckSanctions(userId, SanctionType.NotAllowToEditProfile);

            var currentUser = _userRepository.GetUserById(userId);

            //hardcoded value
            if (newYear > 1900 && newYear < DateTimeOffset.Now.Year || newYear == 0)
                currentUser.UserProfile.BirthYear = newYear;
            else throw new IndexOutOfRangeException();

            _userRepository.Update(currentUser);
        }

        public void BecomeTeacher(int userId)
        {
            CheckSanctions(userId, SanctionType.NotAllowToTeach);

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

        private void CheckSanctions(int userId, SanctionType sanctionType)
        {
            Ensure.Bool.IsFalse(_sanctionRepository.GetAllOfUser(userId).ToList()
                    .Exists(s => s.Type.Equals(sanctionType) && s.IsActive), nameof(CheckSanctions),
                opt => opt.WithException(
                    new ActionIsNotAllowWithSanctionsException(SanctionType.NotAllowToEditProfile)));
        }

        public void EditProfile(int userId, string newName, string newAboutUser, Gender newGender, string newAvatarLink, 
            List<string> newContactData, int newYear)
        {
            CheckSanctions(userId, SanctionType.NotAllowToEditProfile);

            var currentUser = _userRepository.GetUserById(userId);
            currentUser.UserProfile.Name = Ensure.String.IsNotNullOrWhiteSpace(newName);
            currentUser.UserProfile.AboutUser = Ensure.String.IsNotNullOrWhiteSpace(newAboutUser);
            currentUser.UserProfile.Gender = newGender;

            if (newAvatarLink.Length == 0)
            {
                currentUser.UserProfile.AvatarLink = newAvatarLink;
            }
            else
            {
                Ensure.Bool.IsTrue(_fileRepository.DoesFileExists(newAvatarLink), nameof(EditAboutUser),
                    opt => opt.WithException(new FileDoesNotExistException()));
                currentUser.UserProfile.AvatarLink = newAvatarLink;
            }

            Ensure.Any.IsNotNull(newContactData);
            if (newContactData.TrueForAll(d => !string.IsNullOrWhiteSpace(d)))
                currentUser.UserProfile.Contacts = newContactData;
            else throw new ArgumentException();

            if (newYear > 1900 && newYear < DateTimeOffset.Now.Year || newYear == 0)
                currentUser.UserProfile.BirthYear = newYear;
            else throw new IndexOutOfRangeException();

            _userRepository.Update(currentUser);
        }

        public void ConfigureNotificationsSettings(int userId, NotificationType configuringNotification, NotificationValue newValue)
        {
            Ensure.Any.IsNotNull(configuringNotification);
            Ensure.Any.IsNotNull(newValue);

            var currentUser = _userRepository.GetUserById(userId);
            currentUser.NotificationsSettings.ConfigureSettings(configuringNotification, newValue);
            _userRepository.Update(currentUser);
        }
    }
}