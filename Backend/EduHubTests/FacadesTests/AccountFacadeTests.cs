using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Facades;
using EduHubLibrary.Infrastructure;
using EduHubLibrary.Interators;
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
    public class AccountFacadeTests
    {
        private Mock<IEmailSender> _emailSender;
        private IGroupRepository _groupRepository;
        private IKeysRepository _keysRepository;
        private IUserRepository _userRepository;
        private IEventRepository _eventRepository;
        private Mock<IEventPublisher> _publisher;

        [TestInitialize]
        public void Initialize()
        {
            _userRepository = new InMemoryUserRepository();
            _keysRepository = new InMemoryKeysRepository();
            _groupRepository = new InMemoryGroupRepository();
            _eventRepository = new InMemoryEventRepository();
            _publisher = new Mock<IEventPublisher>();
            
            _emailSender = new Mock<IEmailSender>();
        }

        [TestMethod]
        public void RegistrateNewUser_UserWasRegistrated()
        {
            //Arrange
            var accountFacade = new AccountFacade(_keysRepository,
                _userRepository, _emailSender.Object);
            var userFacade = new UserFacade(_userRepository, _groupRepository, _eventRepository, _publisher.Object);
            var expectedName = "yaroslav";
            var expectedPass = "123123";
            var expectedEmail = "bus.yaroslav@gmail.com";
            var expectedType = UserType.UnConfirmed;
            var expectedStatus = false;

            //Act
            var userId = accountFacade.RegUser(expectedName, Credentials.FromRawData(expectedEmail, expectedPass),
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
            var accountFacade = new AccountFacade(_keysRepository, _userRepository, _emailSender.Object);
            var userFacade = new UserFacade(_userRepository, _groupRepository, _eventRepository, _publisher.Object);
            var key = new Key("email", KeyAppointment.BecomeAdmin);
            _keysRepository.AddKey(key);

            //Act
            var userId = accountFacade.RegUser("Alena", Credentials.FromRawData("email", "password"), true, key.Value);
            var currentUser = userFacade.GetUser(userId);

            //Assert
            Assert.AreEqual(UserType.Admin, currentUser.Type);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongKeyAppointmentException))]
        public void RegistrateAdminUsingWrongTypeKey_GetException()
        {
            //Arrange
            var accountFacade = new AccountFacade(_keysRepository, _userRepository, _emailSender.Object);
            var userFacade = new UserFacade(_userRepository, _groupRepository, _eventRepository, _publisher.Object);
            var key = new Key("email", KeyAppointment.ChangePassword);
            _keysRepository.AddKey(key);

            //Act
            var userId = accountFacade.RegUser("Alena", Credentials.FromRawData("email", "password"), true, key.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(KeyAlreadyUsedException))]
        public void RegistrateAdminUsingAlreadyUsedKey_GetException()
        {
            //Arrange
            var accountFacade = new AccountFacade(_keysRepository, _userRepository, _emailSender.Object);
            var userFacade = new UserFacade(_userRepository, _groupRepository, _eventRepository, _publisher.Object);
            var key = new Key("email", KeyAppointment.BecomeAdmin);
            _keysRepository.AddKey(key);

            //Act
            key.UseKey();
            var userId = accountFacade.RegUser("Alena", Credentials.FromRawData("email", "password"), true, key.Value);
        }

        [TestMethod]
        [ExpectedException(typeof(InappropriateEmailException))]
        public void RegAdminWithEmailThatDoesntEqualExpectedEmail_GetException()
        {
            //Arrange
            var accountFacade = new AccountFacade(_keysRepository, _userRepository, _emailSender.Object);
            var userFacade = new UserFacade(_userRepository, _groupRepository, _eventRepository, _publisher.Object);
            var key = new Key("expectedEmail", KeyAppointment.BecomeAdmin);
            _keysRepository.AddKey(key);

            //Act
            var userId = accountFacade.RegUser("Alena", Credentials.FromRawData("anotherEmail", "password"), true, key.Value);
        }

        [TestMethod]
        public void RegistrateModeratorUsingRightKey_ModeratorWasRegistrated()
        {
            //Arrange
            var accountFacade = new AccountFacade(_keysRepository, _userRepository, _emailSender.Object);
            var userFacade = new UserFacade(_userRepository, _groupRepository, _eventRepository, _publisher.Object);
            var key = new Key("email", KeyAppointment.BecomeModerator);
            _keysRepository.AddKey(key);

            //Act
            var userId = accountFacade.RegUser("Alena", Credentials.FromRawData("email", "password"), true, key.Value);
            var currentUser = userFacade.GetUser(userId);

            //Assert
            Assert.AreEqual(UserType.Moderator, currentUser.Type);
        }

        [TestMethod]
        public void ConfirmUserUsingRightKey_GetConfirmedUser()
        {
            //Arrange
            var accountFacade = new AccountFacade(_keysRepository, _userRepository, _emailSender.Object);
            var userFacade = new UserFacade(_userRepository, _groupRepository, _eventRepository, _publisher.Object);
            var key = new Key("email", KeyAppointment.ConfirmEmail);
            _keysRepository.AddKey(key);
            var userId = accountFacade.RegUser("Alena", Credentials.FromRawData("email", "password"), true);

            //Act
            accountFacade.ConfirmUser(key.Value);
            var currentUser = userFacade.GetUser(userId);

            //Assert
            Assert.AreEqual(UserType.User, currentUser.Type);
            Assert.AreEqual(true, key.Used);
        }

        [TestMethod]
        public void ChangePassword_GetChangedPassword()
        {
            //Arrange
            var accountFacade = new AccountFacade(_keysRepository, _userRepository, _emailSender.Object);
            var userFacade = new UserFacade(_userRepository, _groupRepository, _eventRepository, _publisher.Object);
            var expectedPasswordHash = Credentials.FromRawData("someEmail", "newPassword").PasswordHash;
            var userId = accountFacade.RegUser("Alena", Credentials.FromRawData("email", "password"), true);

            //Act
            accountFacade.ChangePassword(userId, "newPassword");
            var currentUser = userFacade.GetUser(userId);

            //Assert
            Assert.AreEqual(expectedPasswordHash, currentUser.Credentials.PasswordHash);
        }

        [TestMethod]
        public void ChangePasswordWithKey_GetChangedPassword()
        {
            //Arrange
            var accountFacade = new AccountFacade(_keysRepository, _userRepository, _emailSender.Object);
            var userFacade = new UserFacade(_userRepository, _groupRepository, _eventRepository, _publisher.Object);
            var expectedPasswordHash = Credentials.FromRawData("someEmail", "newPassword").PasswordHash;
            var userId = accountFacade.RegUser("Alena", Credentials.FromRawData("email", "password"), true);

            var key = new Key("email", KeyAppointment.ChangePassword);
            _keysRepository.AddKey(key);

            //Act
            accountFacade.ChangePassword("newPassword", key.Value);
            var currentUser = userFacade.GetUser(userId);

            //Assert
            Assert.AreEqual(expectedPasswordHash, currentUser.Credentials.PasswordHash);
            Assert.AreEqual(true, key.Used);
        }
    }
}
