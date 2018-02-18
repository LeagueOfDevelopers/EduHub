using System;
using System.Collections.Generic;
using System.Linq;
using EduHubLibrary.Common;
using EduHubLibrary.Domain;
using EduHubLibrary.Facades;
using EduHubLibrary.Infrastructure;
using EduHubLibrary.Settings;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EduHubLibrary.Domain.Exceptions;

namespace EduHubTests
{
    [TestClass]
    public class GroupFacadeTests
    {
        [TestInitialize]
        public void Initialize()
        {
            var inMemoryUserRepository = new InMemoryUserRepository();
            var inMemoryGroupRepository = new InMemoryGroupRepository();
            var inMemoryKeyRepository = new InMemoryKeysRepository();
            var groupSettings = new GroupSettings(2, 10, 0, 1000);
            var emailSettings = new EmailSettings("", "", "", "", "", 4);
            var emailSender = new EmailSender(emailSettings);
            _groupFacade = new GroupFacade(inMemoryGroupRepository, inMemoryUserRepository,
                new GroupSettings(3, 100, 0, 1000));
            _userFacade = new UserFacade(inMemoryUserRepository, inMemoryGroupRepository);
            _authUserFacade = new AuthUserFacade(inMemoryKeyRepository, inMemoryUserRepository,
                emailSender);
            var creatorId = _authUserFacade.RegUser("Alena", new Credentials("email", "password"), true, UserType.User);
            _groupCreator = _userFacade.GetUser(creatorId);
        }

        [TestMethod]
        public void FindGroupUsingTags_GetRightResultWithSorting()
        {
            //Arrange
            var tags1 = new List<string> {"Java", "C++", "C#"};
            var tags2 = new List<string> {"C#", "Pascal", "PHP", "C++"};
            var tags3 = new List<string> {"Delphi", "Java"};

            var createdGroupId1 = _groupFacade.CreateGroup(_groupCreator.Id, "Some group", tags1, "You're welcome!", 3,
                100, false, GroupType.Lecture);
            var createdGroupId2 = _groupFacade.CreateGroup(_groupCreator.Id, "Some group", tags2, "You're welcome!", 3,
                100, false, GroupType.Lecture);
            var createdGroupId3 = _groupFacade.CreateGroup(_groupCreator.Id, "Some group", tags3, "You're welcome!", 3,
                100, false, GroupType.Lecture);

            var requiredTags = new List<string> {"C++", "C#"};

            //Act
            var foundGroups = _groupFacade.FindByTags(requiredTags).ToList();

            //Assert
            Assert.AreEqual(createdGroupId1, foundGroups[0].GroupInfo.Id);
            Assert.AreEqual(createdGroupId2, foundGroups[1].GroupInfo.Id);
        }
        
        [TestMethod]
        public void CreateGroupByGroupFacadeWithValidValues_GroupWasCreated()
        {
            //Arrange
            var creatorId = _authUserFacade.RegUser("Alena", Credentials.FromRawData("Email", "Password"), 
                false, UserType.User);
            var title = "some group";
            var description = "some description";
            var tags = new List<string> { "c#" };
            var size = 3;
            var moneyPerUser = 100.0;

            //Act
            var groupId = _groupFacade.CreateGroup(creatorId, title, tags, description, size, moneyPerUser, 
                false, GroupType.Seminar);

            //Assert
            Assert.IsNotNull(_groupFacade.GetGroup(groupId));
        }

        [TestMethod]
        [ExpectedException(typeof(UserNotFoundException))]
        public void TryToCreateGroupByNotExistingUser_GetException()
        {
            //Arrange
            var invalidUserId = Guid.NewGuid();

            //Act
            _groupFacade.CreateGroup(invalidUserId, "Some group", new List<string> { "c#" },
                "You're welcome!", 3, 100, false, GroupType.Lecture);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void TryToCreateGroupWithInvalidSettings_GetException()
        {
            //Arrange
            var creatorId = _authUserFacade.RegUser("Alena", Credentials.FromRawData("Email", "Password"),
                false, UserType.User);

            //Act
            _groupFacade.CreateGroup(creatorId, "Some group", new List<string> { "c#" },
                "You're welcome!", 2, 2000, false, GroupType.Lecture);
        }

        private User _groupCreator;
        private IGroupFacade _groupFacade;
        private IUserFacade _userFacade;
        private IAuthUserFacade _authUserFacade;
    }
}