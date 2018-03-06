using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Facades;
using EduHubLibrary.Infrastructure;
using EduHubLibrary.Mailing;
using EduHubLibrary.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace EduHubTests.FacadesTests
{
    [TestClass]
    public class AuthUserFacadeTests
    {
        private EmailSender _emailSender;
        private IGroupRepository _groupRepository;
        private IKeysRepository _keysRepository;
        private IUserRepository _userRepository;

        [TestInitialize]
        public void Initialize()
        {
            _userRepository = new InMemoryUserRepository();
            _keysRepository = new InMemoryKeysRepository();
            _groupRepository = new InMemoryGroupRepository();

            var emailSettings = new EmailSettings("", "", "", "", "", 4);
            _emailSender = new EmailSender(emailSettings);
        }

        [TestMethod]
        public void RegistrateNewUser_UserWasRegistrated()
        {
            //Arrange
            var authUserFacade = new AuthUserFacade(_keysRepository,
                _userRepository, _emailSender);
            var userFacade = new UserFacade(_userRepository, _groupRepository, _keysRepository);
            var expectedName = "yaroslav";
            var expectedPass = "123123";
            var expectedEmail = "bus.yaroslav@gmail.com";
            var expectedType = UserType.UnConfirmed;
            var expectedStatus = false;

            //Act
            var userId = authUserFacade.RegUser(expectedName, Credentials.FromRawData(expectedEmail, expectedPass),
                expectedStatus);
            var currentUser = userFacade.GetUser(userId);

            //Assert
            Assert.AreEqual(currentUser.UserProfile.Name, expectedName);
            Assert.AreEqual(currentUser.UserProfile.IsTeacher, expectedStatus);
            Assert.AreEqual(currentUser.Type, expectedType);
        }

        [TestMethod]
        public void RegistrateAdminUsingRightKey_AdminWasRegistrated()
        {
            //Arrange
            var authUserFacade = new AuthUserFacade(_keysRepository, _userRepository, _emailSender);
            var userFacade = new UserFacade(_userRepository, _groupRepository, _keysRepository);
            var key = new Key("email", KeyAppointment.BecomeAdmin);
            _keysRepository.AddKey(key);

            //Act
            var userId = authUserFacade.RegUser("Alena", Credentials.FromRawData("email", "password"), true, key.Value);
            var currentUser = userFacade.GetUser(userId);

            //Assert
            Assert.AreEqual(UserType.Admin, currentUser.Type);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongKeyAppointmentException))]
        public void RegistrateAdminUsingWrongTypeKey_GetException()
        {
            //Arrange
            var authUserFacade = new AuthUserFacade(_keysRepository, _userRepository, _emailSender);
            var userFacade = new UserFacade(_userRepository, _groupRepository, _keysRepository);
            var key = new Key("email", KeyAppointment.ChangePassword);
            _keysRepository.AddKey(key);

            //Act
            var userId = authUserFacade.RegUser("Alena", Credentials.FromRawData("email", "password"), true, key.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyAlreadyUsedException))]
        public void RegistrateAdminUsingAlreadyUsedKey_GetException()
        {
            //Arrange
            var authUserFacade = new AuthUserFacade(_keysRepository, _userRepository, _emailSender);
            var userFacade = new UserFacade(_userRepository, _groupRepository, _keysRepository);
            var key = new Key("email", KeyAppointment.BecomeAdmin);
            _keysRepository.AddKey(key);

            //Act
            key.UseKey();
            var userId = authUserFacade.RegUser("Alena", Credentials.FromRawData("email", "password"), true, key.Value);
        }

        [TestMethod]
        public void RegistrateModeratorUsingRightKey_ModeratorWasRegistrated()
        {
            //Arrange
            var authUserFacade = new AuthUserFacade(_keysRepository, _userRepository, _emailSender);
            var userFacade = new UserFacade(_userRepository, _groupRepository, _keysRepository);
            var key = new Key("email", KeyAppointment.BecomeModerator);
            _keysRepository.AddKey(key);

            //Act
            var userId = authUserFacade.RegUser("Alena", Credentials.FromRawData("email", "password"), true, key.Value);
            var currentUser = userFacade.GetUser(userId);

            //Assert
            Assert.AreEqual(UserType.Moderator, currentUser.Type);
        }

        [TestMethod]
        public void ConfirmUserUsingRightKey_GetConfirmedUser()
        {
            //Arrange
            var authUserFacade = new AuthUserFacade(_keysRepository, _userRepository, _emailSender);
            var userFacade = new UserFacade(_userRepository, _groupRepository, _keysRepository);
            var key = new Key("email", KeyAppointment.ConfirmEmail);
            _keysRepository.AddKey(key);
            var userId = authUserFacade.RegUser("Alena", Credentials.FromRawData("email", "password"), true);

            //Act
            authUserFacade.ConfirmUser(key.Value);
            var currentUser = userFacade.GetUser(userId);

            //Assert
            Assert.AreEqual(UserType.User, currentUser.Type);
            Assert.AreEqual(true, key.Used);
        }
    }
}
