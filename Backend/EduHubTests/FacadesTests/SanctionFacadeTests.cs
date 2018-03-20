﻿using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Facades;
using EduHubLibrary.Infrastructure;
using EduHubLibrary.Interators;
using EduHubLibrary.Mailing;
using EduHubLibrary.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace EduHubTests.FacadesTests
{
    [TestClass]
    public class SanctionFacadeTests
    {
        private int _adminId;
        private int _testUserId;
        private IUserFacade _userFacade;
        private IAccountFacade _accountFacade;
        private IUserRepository _userRepository;
        private ISanctionRepository _sanctionRepository;

        [TestInitialize]
        public void Initialize()
        {
            var emailSettings = new EmailSettings("", "", "", "", "", 4);
            var emailSender = new EmailSender(emailSettings);
            var keysRepository = new InMemoryKeysRepository();
            var groupRepository = new InMemoryGroupRepository();
            var fileRepository = new InMemoryFileRepository();
            var adminKey = new Key("ivanov@mail.ru", KeyAppointment.BecomeAdmin);
            keysRepository.AddKey(adminKey);

            _userRepository = new InMemoryUserRepository();
            _sanctionRepository = new InMemorySanctionRepository();
            _accountFacade = new AccountFacade(keysRepository, _userRepository, emailSender);
            _userFacade = new UserFacade(_userRepository, groupRepository, keysRepository);
            _adminId = _accountFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false, adminKey.Value);
            _testUserId = _accountFacade.RegUser("Sasha", Credentials.FromRawData("smt@smt.ru", "2"), false);
        }

        [TestMethod]
        public void AddSanction_GetAddedSanction()
        {
            //Arrange
            var sanctionFacade = new SanctionFacade(_sanctionRepository, _userRepository);

            //Act
            sanctionFacade.AddSanction("Some rule", _testUserId, _adminId, SanctionType.NotAllowToEditProfile);

            //Assert
            Assert.AreEqual(1, sanctionFacade.GetAll().ToList().Count);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TryToAddSanctionWithInvalidExpirationDate_GetException()
        {
            //Arrange
            var sanctionFacade = new SanctionFacade(_sanctionRepository, _userRepository);

            //Act
            sanctionFacade.AddSanction("Some rule", _testUserId, _adminId, 
                SanctionType.NotAllowToEditProfile, DateTimeOffset.Now);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void TryToAddSanctionToNotExistingUser_GetException()
        {
            //Arrange
            var sanctionFacade = new SanctionFacade(_sanctionRepository, _userRepository);

            //Act
            sanctionFacade.AddSanction("some rule", IntIterator.GetNextId(), _adminId, SanctionType.NotAllowToEditProfile);
        }

        [TestMethod]
        [ExpectedException(typeof(NotEnoughPermissionsException))]
        public void TryToAddSanctionByNotModerator_GetException()
        {
            //Arrange
            var sanctionFacade = new SanctionFacade(_sanctionRepository, _userRepository);
            var pseudoModerator = _accountFacade.RegUser("not admin", Credentials.FromRawData("pseudo@email.ru", "password"), false);
            
            //Act
            sanctionFacade.AddSanction("some rule", _testUserId, pseudoModerator, SanctionType.NotAllowToEditProfile);
        }

        [TestMethod]
        public void CancelSanction_GetCanceledSanction()
        {
            //Arrange
            var sanctionFacade = new SanctionFacade(_sanctionRepository, _userRepository);
            var sanctionId = sanctionFacade.AddSanction("some rule", _testUserId, _adminId, SanctionType.NotAllowToEditProfile);

            //Act
            sanctionFacade.CancelSanction(sanctionId);

            //Assert
            Assert.AreEqual(false, _sanctionRepository.Get(sanctionId).IsActive);
        }

        [TestMethod]
        [ExpectedException(typeof(SanctionNotFoundException))]
        public void CancelNotExistingSanction_GetException()
        {
            //Arrange
            var sanctionFacade = new SanctionFacade(_sanctionRepository, _userRepository);

            //Act
            sanctionFacade.CancelSanction(IntIterator.GetNextId());
        }

        [TestMethod]
        public void GetAllSanctionsOfModerator_GetRightList()
        {
            //Arrange
            var sanctionFacade = new SanctionFacade(_sanctionRepository, _userRepository);

            var user1 = _accountFacade.RegUser("not admin", Credentials.FromRawData("user1@email.ru", "password"), false);
            var user2 = _accountFacade.RegUser("not admin", Credentials.FromRawData("user2@email.ru", "password"), false);
            var user3 = _accountFacade.RegUser("not admin", Credentials.FromRawData("user3@email.ru", "password"), false);

            sanctionFacade.AddSanction("some rule", user1, _adminId, SanctionType.NotAllowToJoinGroup);
            sanctionFacade.AddSanction("some rule", user2, _adminId, SanctionType.NotAllowToEditProfile);
            sanctionFacade.AddSanction("some rule", user3, _adminId, SanctionType.NotAllowToTeach);

            //Act
            var actual = sanctionFacade.GetAllOfModerator(_adminId).ToList();

            //Assert
            Assert.AreEqual(user1, actual[0].UserId);
            Assert.AreEqual(user2, actual[1].UserId);
            Assert.AreEqual(user3, actual[2].UserId);
        }

        [TestMethod]
        [ExpectedException(typeof(NotEnoughPermissionsException))]
        public void TryToGetAllSanctionsOfNotModerator_GetException()
        {
            //Arrange
            var sanctionFacade = new SanctionFacade(_sanctionRepository, _userRepository);

            //Act
            var actual = sanctionFacade.GetAllOfModerator(_testUserId).ToList();
        }

        [TestMethod]
        public void GetAllSanctionsOfUser_GetRightList()
        {
            //Arrange
            var sanctionFacade = new SanctionFacade(_sanctionRepository, _userRepository);

            sanctionFacade.AddSanction("some rule", _testUserId, _adminId, SanctionType.NotAllowToJoinGroup);
            sanctionFacade.AddSanction("some rule", _testUserId, _adminId, SanctionType.NotAllowToEditProfile);
            sanctionFacade.AddSanction("some rule", _testUserId, _adminId, SanctionType.NotAllowToTeach);

            //Act
            var actual = sanctionFacade.GetAllOfModerator(_adminId).ToList();

            //Assert
            Assert.AreEqual(SanctionType.NotAllowToJoinGroup, actual[0].Type);
            Assert.AreEqual(SanctionType.NotAllowToEditProfile, actual[1].Type);
            Assert.AreEqual(SanctionType.NotAllowToTeach, actual[2].Type);
        }

        [TestMethod]
        public void CheckActivityOfExpiredSanction_GetInactiveSanction()
        {
            //Arrange
            var sanctionFacade = new SanctionFacade(_sanctionRepository, _userRepository);
            var sanctionId = sanctionFacade.AddSanction("Some rule", _testUserId, _adminId, 
                SanctionType.NotAllowToEditProfile, DateTimeOffset.Now.AddMilliseconds(1));

            //Act
            Thread.Sleep(2);

            //Assert
            Assert.AreEqual(false, sanctionFacade.GetAll().ToList()[0].IsActive);
        }
    }
}