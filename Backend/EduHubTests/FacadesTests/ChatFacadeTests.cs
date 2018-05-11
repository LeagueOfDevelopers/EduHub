using System;
using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.NotificationService;
using EduHubLibrary.Facades;
using EduHubLibrary.Infrastructure;
using EduHubLibrary.Mailing;
using EduHubLibrary.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace EduHubTests
{
    [TestClass]
    public class ChatFacadeTests
    {
        private int _creatorId;
        private IGroupRepository _groupRepository;
        private int _testGroupId;
        private IUserRepository _userRepository;
        private UserSettings userSettings;

        [TestInitialize]
        public void Initialize()
        {
            var keyRepository = new InMemoryKeysRepository();
            _userRepository = new InMemoryUserRepository();
            var sanctionRepository = new InMemorySanctionRepository();
            _groupRepository = new InMemoryGroupRepository();
            var publisher = new Mock<IEventPublisher>();
            var groupSettings = new GroupSettings(3, 100, 100, 1000);
            var sender = new Mock<IEmailSender>();
            var groupFacade = new GroupFacade(_groupRepository, _userRepository, sanctionRepository, groupSettings,
                publisher.Object);
            userSettings = new UserSettings("");
            var accountFacade = new AccountFacade(keyRepository, _userRepository,
                sender.Object, userSettings);

            _creatorId = accountFacade.RegUser("Alena", Credentials.FromRawData("email", "password"), true);
            _testGroupId = groupFacade.CreateGroup(_creatorId, "Some group", new List<string> {"c#"}, "Interesting",
                3, 100, false, GroupType.Lecture);
        }

        [TestMethod]
        public void SendMessageToChat_GetOneMessageInListInChat()
        {
            //Arrange
            var chatFacade = new ChatFacade(_groupRepository, _userRepository);

            //Act
            chatFacade.SendMessage(_creatorId, _testGroupId, "Some message");

            //Arrange
            Assert.AreEqual(1, _groupRepository.GetGroupById(_testGroupId).Messages.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void SendInvalidMessageToChat_GetException()
        {
            //Arrange
            var chatFacade = new ChatFacade(_groupRepository, _userRepository);

            //Act
            chatFacade.SendMessage(_creatorId, _testGroupId, " ");
        }
    }
}