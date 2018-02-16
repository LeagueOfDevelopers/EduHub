using System;
using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using EduHubLibrary.Facades;
using EduHubLibrary.Infrastructure;
using EduHubLibrary.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EduHubTests
{
    [TestClass]
    public class ChatFacadeTests
    {
        private Guid _creatorId;
        private InMemoryGroupRepository _groupRepository;
        private Guid _testGroupId;

        [TestInitialize]
        public void Initialize()
        {
            _groupRepository = new InMemoryGroupRepository();

            var userRepository = new InMemoryUserRepository();
            var groupSettings = new GroupSettings(3, 100, 100, 1000);
            var groupFacade = new GroupFacade(_groupRepository, userRepository, groupSettings);
            var userFacade = new AuthUserFacade(userRepository, _groupRepository);

            _creatorId = userFacade.RegUser("Alena", Credentials.FromRawData("email", "password"), true, UserType.User);
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
            Assert.AreEqual(1, _groupRepository.GetGroupById(_testGroupId).Chat.Messages.Count());
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

        [TestMethod]
        public void EditMessageInChat_GetEditedMessageInChat()
        {
            //Arrange
            var chatFacade = new ChatFacade(_groupRepository);
            var messageId = chatFacade.SendMessage(_creatorId, _testGroupId, "Some message");

            //Act
            var newText = "New text";
            chatFacade.EditMessage(_creatorId, messageId, _testGroupId, newText);

            //Assert
            var newMessage = _groupRepository.GetGroupById(_testGroupId).Chat.GetMessage(messageId);
            Assert.AreEqual(newText, newMessage.Text);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EditMessageWithInvalidValueInChat_GetException()
        {
            //Arrange
            var chatFacade = new ChatFacade(_groupRepository);
            var messageId = chatFacade.SendMessage(_creatorId, _testGroupId, "Some message");

            //Act
            chatFacade.EditMessage(_creatorId, messageId, _testGroupId, " ");
        }

        [TestMethod]
        [ExpectedException(typeof(NotEnoughPermissionsException))]
        public void TryToEditMessageByNotSender_GetException()
        {
            //Arrange
            var chatFacade = new ChatFacade(_groupRepository);
            var messageId = chatFacade.SendMessage(_creatorId, _testGroupId, "Some message");

            //Act
            chatFacade.EditMessage(Guid.NewGuid(), messageId, _testGroupId, "New text");
        }

        [TestMethod]
        public void DeleteMessageInChat_GetEmptyListOfMessages()
        {
            //Arrange
            var chatFacade = new ChatFacade(_groupRepository);
            var messageId = chatFacade.SendMessage(_creatorId, _testGroupId, "Some message");

            //Act
            chatFacade.DeleteMessage(_creatorId, messageId, _testGroupId);

            //Assert
            Assert.AreEqual(0, _groupRepository.GetGroupById(_testGroupId).Chat.Messages.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(NotEnoughPermissionsException))]
        public void TryToDeleteMessageByNotSender_GetException()
        {
            //Arrange
            var chatFacade = new ChatFacade(_groupRepository);
            var messageId = chatFacade.SendMessage(_creatorId, _testGroupId, "Some message");

            //Act
            chatFacade.DeleteMessage(Guid.NewGuid(), messageId, _testGroupId);
        }
    }
}