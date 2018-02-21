using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Tools;
using EnsureThat;
using System;
using System.Collections.Generic;
using EduHubLibrary.Domain.Exceptions;

namespace EduHubLibrary.Facades
{
    public class UserEditFacade : IUserEditFacade
    {
        public UserEditFacade(IUserRepository userRepository,
            IFileRepository fileRepository)
        {
            _userRepository = userRepository;
            _fileRepository = fileRepository;
        }

        public void EditName(Guid userId, string newName)
        {
            var currentUser = _userRepository.GetUserById(userId);
            currentUser.UserProfile.Name = Ensure.String.IsNotNullOrWhiteSpace(newName);
            _userRepository.Update(currentUser);
        }

        public void EditAboutUser(Guid userId, string newAboutUser)
        {
            var currentUser = _userRepository.GetUserById(userId);
            currentUser.UserProfile.AboutUser = Ensure.String.IsNotNullOrWhiteSpace(newAboutUser);
            _userRepository.Update(currentUser);
        }

        public void EditGender(Guid userId, Gender gender)
        {
            var currentUser = _userRepository.GetUserById(userId);
            currentUser.UserProfile.Gender = gender;
            _userRepository.Update(currentUser);
        }

        public void EditAvatarLink(Guid userId, string newAvatarLink)
        {
            Ensure.String.IsNotNullOrWhiteSpace(newAvatarLink);
            Ensure.Bool.IsTrue(_fileRepository.DoesFileExists(newAvatarLink),
                nameof(EditAboutUser), 
                opt => opt.WithException(new FileDoesNotExistException()));

            var currentUser = _userRepository.GetUserById(userId);
            currentUser.UserProfile.AvatarLink = newAvatarLink; 
            _userRepository.Update(currentUser);
        }

        public void EditContacts(Guid userId, List<string> newContactData)
        {
            var currentUser = _userRepository.GetUserById(userId);

            if (newContactData.Count != 0 && newContactData.TrueForAll(d => !string.IsNullOrWhiteSpace(d)))
                currentUser.UserProfile.Contacts = newContactData;
            else throw new ArgumentException();

            _userRepository.Update(currentUser);
        }

        public void EditBirthYear(Guid userId, int newYear)
        {
            var currentUser = _userRepository.GetUserById(userId);

            //hardcoded value
            if (newYear > 1900 && newYear < DateTimeOffset.Now.Year) currentUser.UserProfile.BirthYear = newYear;
            else throw new IndexOutOfRangeException();

            _userRepository.Update(currentUser);
        }

        public void BecomeTeacher(Guid userId)
        {
            var currentUser = _userRepository.GetUserById(userId);
            currentUser.UserProfile.IsTeacher = true;
            _userRepository.Update(currentUser);
        }

        public void StopToBeTeacher(Guid userId)
        {
            var currentUser = _userRepository.GetUserById(userId);
            currentUser.UserProfile.IsTeacher = false;
            _userRepository.Update(currentUser);
        }

        private readonly IUserRepository _userRepository;
        private readonly IFileRepository _fileRepository;
    }
}
