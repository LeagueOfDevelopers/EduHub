using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Facades;
using EduHubLibrary.Infrastructure;
using EduHubLibrary.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EduHubTests
{
    [TestClass]
    public class ChatFacadeTests
    {
        private InMemoryGroupRepository _groupRepository;
        private Guid _testGroupId;
        private Guid _creatorId;

        [TestInitialize]
        public void Initialize()
        {
            _groupRepository = new InMemoryGroupRepository();

            var userRepository = new InMemoryUserRepository();
            var groupSettings = new GroupSettings(3, 100, 100, 1000);
            var groupFacade = new GroupFacade(_groupRepository, userRepository, groupSettings);
            var userFacade = new UserFacade(userRepository, _groupRepository);
            
            _creatorId = userFacade.RegUser("Alena", Credentials.FromRawData("email", "password"), true, UserType.User);
            _testGroupId = groupFacade.CreateGroup(_creatorId, "Some group", new List<string> { "c#" }, "Interesting",
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
            chatFacade.EditMessage(messageId, _testGroupId, newText);

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
            chatFacade.EditMessage(messageId, _testGroupId, " ");
        }

        [TestMethod]
        public void DeleteMessageInChat_GetEmptyListOfMessages()
        {
            //Arrange
            var chatFacade = new ChatFacade(_groupRepository);
            var messageId = chatFacade.SendMessage(_creatorId, _testGroupId, "Some message");

            //Act
            chatFacade.DeleteMessage(messageId, _testGroupId);

            //Assert
            Assert.AreEqual(0, _groupRepository.GetGroupById(_testGroupId).Chat.Messages.Count());
        }
    }
}
