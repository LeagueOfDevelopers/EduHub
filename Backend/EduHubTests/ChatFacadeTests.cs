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
        private InMemoryUserRepository _userRepository;
        private GroupSettings _groupSettings;
        private GroupFacade _groupFacade;
        private UserFacade _userFacade;
        private Guid _testGroupId;
        private Guid _creatorId;

        [TestInitialize]
        public void Initialize()
        {
            _groupRepository = new InMemoryGroupRepository();
            _userRepository = new InMemoryUserRepository();
            _groupSettings = new GroupSettings(3, 100, 100, 1000);
            _groupFacade = new GroupFacade(_groupRepository, _userRepository, _groupSettings);
            _userFacade = new UserFacade(_userRepository, _groupRepository);

            _creatorId = _userFacade.RegUser("Alena", Credentials.FromRawData("email", "password"), true, UserType.User);
            _testGroupId = _groupFacade.CreateGroup(_creatorId, "Some group", new List<string> { "c#" }, "Interesting",
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
            Assert.AreEqual(1, _groupRepository.GetGroupById(_testGroupId).Chat.Messages.ToList().Count);
        }
    }
}
