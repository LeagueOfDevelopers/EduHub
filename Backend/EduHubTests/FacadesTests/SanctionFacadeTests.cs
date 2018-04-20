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
        private Mock<IEventPublisher> _publisher;

        [TestInitialize]
        public void Initialize()
        {
            var emailSender = new Mock<IEmailSender>();
            var keysRepository = new InMemoryKeysRepository();
            var groupRepository = new InMemoryGroupRepository();
            var fileRepository = new InMemoryFileRepository();
            var adminKey = new Key("ivanov@mail.ru", KeyAppointment.BecomeAdmin);
            keysRepository.AddKey(adminKey);

            _publisher = new Mock<IEventPublisher>();
            _userRepository = new InMemoryUserRepository();
            _sanctionRepository = new InMemorySanctionRepository();
            _accountFacade = new AccountFacade(keysRepository, _userRepository, emailSender.Object);
            _userFacade = new UserFacade(_userRepository, groupRepository, keysRepository, _publisher.Object);
            _adminId = _accountFacade.RegUser("Ivan", Credentials.FromRawData("ivanov@mail.ru", "1"), false, adminKey.Value);
            _testUserId = _accountFacade.RegUser("Sasha", Credentials.FromRawData("smt@smt.ru", "2"), false);
        }

        [TestMethod]
        public void AddSanction_GetAddedSanction()
        {
            //Arrange
            var sanctionFacade = new SanctionFacade(_sanctionRepository, _userRepository, _publisher.Object);

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
            var sanctionFacade = new SanctionFacade(_sanctionRepository, _userRepository, _publisher.Object);

            //Act
            sanctionFacade.AddSanction("Some rule", _testUserId, _adminId, 
                SanctionType.NotAllowToEditProfile, DateTimeOffset.Now);
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void TryToAddSanctionToNotExistingUser_GetException()
        {
            //Arrange
            var sanctionFacade = new SanctionFacade(_sanctionRepository, _userRepository, _publisher.Object);

            //Act
            sanctionFacade.AddSanction("some rule", IntIterator.GetNextId(), _adminId, SanctionType.NotAllowToEditProfile);
        }

        [TestMethod]
        [ExpectedException(typeof(NotEnoughPermissionsException))]
        public void TryToAddSanctionByNotModerator_GetException()
        {
            //Arrange
            var sanctionFacade = new SanctionFacade(_sanctionRepository, _userRepository, _publisher.Object);
            var pseudoModerator = _accountFacade.RegUser("not admin", Credentials.FromRawData("pseudo@email.ru", "password"), false);
            
            //Act
            sanctionFacade.AddSanction("some rule", _testUserId, pseudoModerator, SanctionType.NotAllowToEditProfile);
        }

        [TestMethod]
        public void CancelSanction_GetCanceledSanction()
        {
            //Arrange
            var sanctionFacade = new SanctionFacade(_sanctionRepository, _userRepository, _publisher.Object);
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
            var sanctionFacade = new SanctionFacade(_sanctionRepository, _userRepository, _publisher.Object);

            //Act
            sanctionFacade.CancelSanction(IntIterator.GetNextId());
        }

        [TestMethod]
        public void GetAllActiveSanctions_GetRightList()
        {
            //Arrange
            var sanctionFacade = new SanctionFacade(_sanctionRepository, _userRepository, _publisher.Object);

            var user1 = _accountFacade.RegUser("not admin", Credentials.FromRawData("user1@email.ru", "password"), false);
            var user2 = _accountFacade.RegUser("not admin", Credentials.FromRawData("user2@email.ru", "password"), false);
            var user3 = _accountFacade.RegUser("not admin", Credentials.FromRawData("user3@email.ru", "password"), false);

            sanctionFacade.AddSanction("some rule", user1, _adminId, SanctionType.NotAllowToJoinGroup);
            sanctionFacade.AddSanction("some rule", user2, _adminId, SanctionType.NotAllowToEditProfile);
            var canceledSanctionId = sanctionFacade.AddSanction("some rule", user3, _adminId, SanctionType.NotAllowToTeach);
            sanctionFacade.CancelSanction(canceledSanctionId);

            //Act
            var actual = sanctionFacade.GetAllActive().ToList();

            //Assert
            Assert.AreEqual(user1, actual[0].UserId);
            Assert.AreEqual(user2, actual[1].UserId);
        }
        
        [TestMethod]
        public void CheckActivityOfExpiredSanction_GetInactiveSanction()
        {
            //Arrange
            var sanctionFacade = new SanctionFacade(_sanctionRepository, _userRepository, _publisher.Object);
            var sanctionId = sanctionFacade.AddSanction("Some rule", _testUserId, _adminId, 
                SanctionType.NotAllowToEditProfile, DateTimeOffset.Now.AddMilliseconds(1));

            //Act
            Thread.Sleep(4);

            //Assert
            Assert.AreEqual(false, sanctionFacade.GetAll().ToList()[0].IsActive);
        }

        [TestMethod]
        public void GetAllActiveSanctionsOfUser_GetRightResult()
        {
            //Arrange
            var sanctionFacade = new SanctionFacade(_sanctionRepository, _userRepository, _publisher.Object);

            var sanctionId1 =  sanctionFacade.AddSanction("Some rule", _testUserId, _adminId, SanctionType.NotAllowToEditProfile);
            var sanctionId2 = sanctionFacade.AddSanction("Some rule", _testUserId, _adminId, SanctionType.NotAllowToEditProfile);
            var sanctionId3 = sanctionFacade.AddSanction("Some rule", _testUserId, _adminId, SanctionType.NotAllowToEditProfile);

            sanctionFacade.CancelSanction(sanctionId3);

            //Act
            var result = sanctionFacade.GetAllActiveOfUser(_testUserId).ToList();

            //Assert
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(sanctionId1, result[0].Id);
            Assert.AreEqual(sanctionId2, result[1].Id);
        }
    }
}
