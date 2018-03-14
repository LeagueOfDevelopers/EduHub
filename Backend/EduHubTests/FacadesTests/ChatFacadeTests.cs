﻿using System;
using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Facades;
using EduHubLibrary.Infrastructure;
using EduHubLibrary.Mailing;
using EduHubLibrary.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using EduHubLibrary.Domain.NotificationService;

namespace EduHubTests
{
    [TestClass]
    public class ChatFacadeTests
    {
        private int _creatorId;
        private IGroupRepository _groupRepository;
        private int _testGroupId;

        [TestInitialize]
        public void Initialize()
        {
            var keyRepository = new InMemoryKeysRepository();
            var userRepository = new InMemoryUserRepository();
            var sanctionRepository = new InMemorySanctionRepository();
            _groupRepository = new InMemoryGroupRepository();
            var publisher = new Mock<IEventPublisher>();
            var groupSettings = new GroupSettings(3, 100, 100, 1000);
            var emailSettings = new EmailSettings("", "", "", "", "", 3);
            var sender = new EmailSender(emailSettings);
            var groupFacade = new GroupFacade(_groupRepository, userRepository, sanctionRepository, groupSettings, 
                publisher.Object);
            var accountFacade = new AccountFacade(keyRepository, userRepository,
                sender);

            _creatorId = accountFacade.RegUser("Alena", Credentials.FromRawData("email", "password"), true);
            _testGroupId = groupFacade.CreateGroup(_creatorId, "Some group", new List<string> {"c#"}, "Interesting",
                3, 100, false, GroupType.Lecture);
        }

        [TestMethod]
        public void SendMessageToChat_GetOneMessageInListInChat()
        {
            //Arrange
            var chatFacade = new ChatFacade(_groupRepository);

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
            var chatFacade = new ChatFacade(_groupRepository);

            //Act
            chatFacade.SendMessage(_creatorId, _testGroupId, " ");
        }
    }
}