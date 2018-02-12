using System;
using System.Linq;
using EduHubLibrary.Domain;
using EduHubLibrary.Domain.Exceptions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EduHubTests
{
    [TestClass]
    public class ChatTests
    {
        [TestMethod]
        public void SendMessageToChat_GetMessageInListOfMessages()
        {
            //Arrange
            var testChat = new Chat();
            var messageText = "Some message";
            var senderId = Guid.NewGuid();
            var groupId = Guid.NewGuid();

            //Act
            var messageId = testChat.SendMessage(senderId, groupId, messageText);

            //Assert
            var gotMessage = testChat.GetMessage(messageId);
            Assert.AreEqual(messageText, gotMessage.Text);
            Assert.AreEqual(senderId, gotMessage.SenderId);
            Assert.AreEqual(groupId, gotMessage.ReceiverId);
        }

        [TestMethod]
        public void DeleteMessageWithValidIdFromChat_GetEmptyListOfMessages()
        {
            //Arrange
            var testChat = new Chat();
            var messageId = testChat.SendMessage(Guid.NewGuid(), Guid.NewGuid(), "Some message");

            //Act
            testChat.DeleteMessage(messageId);

            //Assert
            var messageList = testChat.Messages.ToList();
            Assert.AreEqual(0, messageList.Count);
        }

        [TestMethod]
        [ExpectedException(typeof(MessageNotFoundException))]
        public void DeleteMessageWithInValidIdFromChat_GetException()
        {
            //Arrange
            var testChat = new Chat();
            testChat.SendMessage(Guid.NewGuid(), Guid.NewGuid(), "Some message");

            var invalidGuid = Guid.NewGuid();

            //Act
            testChat.DeleteMessage(invalidGuid);
        }

        [TestMethod]
        public void EditMessageWithValidIdInChat_GetEditedMessage()
        {
            //Arrange
            var testChat = new Chat();
            var oldText = "Old";
            var messageId = testChat.SendMessage(Guid.NewGuid(), Guid.NewGuid(), oldText);

            //Act
            var newText = "New";
            testChat.EditMessage(messageId, newText);
            var newMessage = testChat.GetMessage(messageId);

            //Assert
            Assert.AreEqual(newText, newMessage.Text);
        }

        [TestMethod]
        [ExpectedException(typeof(MessageNotFoundException))]
        public void EditMessageWithInValidIdFromChat_GetException()
        {
            //Arrange
            var testChat = new Chat();
            testChat.SendMessage(Guid.NewGuid(), Guid.NewGuid(), "Some message");

            var invalidGuid = Guid.NewGuid();

            //Act
            testChat.EditMessage(invalidGuid, "New text");
        }
    }
}